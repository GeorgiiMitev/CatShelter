using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CatShelter.Data;

namespace CatShelter.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<CatShelter.Data.Donation> Donation { get; set; } = default!;
    }
}