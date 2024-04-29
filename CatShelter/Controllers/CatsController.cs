using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatShelter.Data;
using Microsoft.IdentityModel.Tokens;

namespace CatShelter.Controllers
{
    public class CatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Wines
        public async Task<IActionResult> Index(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Cats.ToListAsync());
            }

            if (_context.Cats == null)
            {
                return Problem("Context is empty");
            }

            var cats = from m in _context.Cats select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                cats = cats.Where(s => s.Name.Contains(searchString));
            }
            return View(cats.ToList());
            //var applicationDbContext = _context.Wines
            //.Include(w => w.WineCattegories)
            //.Include(w => w.WineTypes);
            //return View(await applicationDbContext.ToListAsync());
        }
        //// GET: Cats
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = await _context.Cats
        //        .Include(c => c.Breeds)
        //        .Include(c => c.Cages)
        //        .Include(c => c.Vaccines).ToListAsync();
        //    applicationDbContext = applicationDbContext.Select(x =>
        //    {
        //        if (x.ImageURL.Contains(';'))
        //        {
        //            x.ImageURL = x.ImageURL.Substring(0, x.ImageURL.IndexOf(';'));
        //        }
        //        return x;

        //    }).ToList();
        //    return View(applicationDbContext);
        //}

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breeds)
                .Include(c => c.Cages)
                .Include(c => c.Vaccines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            ViewData["images"] = cat.ImageURL.Split(';').ToList();
            return View(cat);
        }

        // GET: Cats/Create
        public IActionResult Create()
        {
            ViewData["BreedsId"] = new SelectList(_context.Breeds, "Id", "BreedName");
            ViewData["CagesId"] = new SelectList(_context.Cages, "Id", "CageNumber");
            ViewData["VaccinesId"] = new SelectList(_context.Vaccines, "Id", "Name");
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Color,BreedsId,VaccinesId,CagesId,ImageURL,Description")] Cat cat)
        {
            cat.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["BreedsId"] = new SelectList(_context.Breeds, "Id", "BreedName", cat.BreedsId);
                ViewData["CagesId"] = new SelectList(_context.Cages, "Id", "CageNumber", cat.CagesId);
                ViewData["VaccinesId"] = new SelectList(_context.Vaccines, "Id", "Name", cat.VaccinesId);
                return View(cat);
            }
            _context.Add(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            ViewData["BreedsId"] = new SelectList(_context.Breeds, "Id", "BreedName", cat.BreedsId);
            ViewData["CagesId"] = new SelectList(_context.Cages, "Id", "CageNumber", cat.CagesId);
            ViewData["VaccinesId"] = new SelectList(_context.Vaccines, "Id", "Name", cat.VaccinesId);
            return View(cat);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Color,BreedsId,VaccinesId,CagesId,ImageURL,Date,Description")] Cat cat)
        {
            cat.Date = DateTime.Now;
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(cat.Id))
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
            ViewData["BreedsId"] = new SelectList(_context.Breeds, "Id", "BreedName", cat.BreedsId);
            ViewData["CagesId"] = new SelectList(_context.Cages, "Id", "CageNumber", cat.CagesId);
            ViewData["VaccinesId"] = new SelectList(_context.Vaccines, "Id", "Name", cat.VaccinesId);
            return View(cat);
        }

        // GET: Cats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breeds)
                .Include(c => c.Cages)
                .Include(c => c.Vaccines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cats'  is null.");
            }
            var cat = await _context.Cats.FindAsync(id);
            if (cat != null)
            {
                _context.Cats.Remove(cat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
            return (_context.Cats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
