using System.ComponentModel.DataAnnotations.Schema;

namespace CatShelter.Data
{
    public class Donation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Payment { get; set; }
    }
}
