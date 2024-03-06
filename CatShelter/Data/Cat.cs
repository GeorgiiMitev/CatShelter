using System.ComponentModel;

namespace CatShelter.Data
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public enum Sex { M, F }
        public int BreedsId { get; set; }
        public Breed Breeds { get; set; }
        public int VaccinesId { get; set; }
        public Vaccine Vaccines { get; set; }
        public int CagesId { get; set; }
        public Cage Cages { get; set; }
        [DisplayName("Image")]
        public string ImageURL { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }
    }
}
