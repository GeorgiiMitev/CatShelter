using Microsoft.AspNetCore.Identity;

namespace CatShelter.Data
{
    public class Client : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Adoption> Adoptions { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
