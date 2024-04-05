using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatShelter.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CatShelter.Controllers
{
    [Authorize]
    public class AdoptionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;
        public AdoptionsController(ApplicationDbContext context, UserManager<Client> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Adoptions
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Adoptions
                    .Include(a => a.Clients)
                    .Include(a => a.Cats);

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Adoptions
                    .Include(a => a.Clients)
                    .Include(a => a.Cats)
                    .Where(x => x.ClientsId == _userManager.GetUserId(User));
                return View(await applicationDbContext.ToListAsync());
            }


        }

        // GET: Adoptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Cats)
                .Include(a => a.Clients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Adoptions/Create
        public IActionResult Create()
        {
            ViewData["CatsId"] = new SelectList(_context.Cats, "Id", "Name");
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Adoptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientsId,CatsId,Description,AdoptionDate")] Adoption adoption)
        {
            adoption.AdoptionDate = DateTime.Now;
            adoption.ClientsId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                ViewData["CatsId"] = new SelectList(_context.Cats, "Id", "Id", adoption.CatsId);
                ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", adoption.ClientsId);
                return View(adoption);

            }
            _context.Add(adoption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateCatAdoption(int catId)
        {
            Adoption adoption = new Adoption();
            adoption.CatsId = catId;
            adoption.AdoptionDate = DateTime.Now;
            adoption.Description = "";
            adoption.ClientsId = _userManager.GetUserId(User);

            _context.Adoptions.Add(adoption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Adoptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }
            ViewData["CatsId"] = new SelectList(_context.Cats, "Id", "Name", adoption.CatsId);
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", adoption.ClientsId);
            return View(adoption);
        }

        // POST: Adoptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatsId,Description")] Adoption adoption)
        {
            adoption.ClientsId = _userManager.GetUserId(User);
            adoption.AdoptionDate = DateTime.Now;
            if (id != adoption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Adoptions.Update(adoption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatsId"] = new SelectList(_context.Cats, "Id", "Name", adoption.CatsId);
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", adoption.ClientsId);
            return View(adoption);
        }

        // GET: Adoptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Cats)
                .Include(a => a.Clients)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adoptions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Adoptions'  is null.");
            }
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _context.Adoptions.Remove(adoption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return (_context.Adoptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
