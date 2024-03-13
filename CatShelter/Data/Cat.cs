using System.ComponentModel;

namespace CatShelter.Data
{
    public class Cat
    {
        public int Id { get; set; }
        [DisplayName("Име")]
        public string Name { get; set; }
        [DisplayName("Възраст")]
        public int Age { get; set; }
        [DisplayName("Цвят")]
        public string Color { get; set; }
        public enum Sex { M, F }

        public int BreedsId { get; set; }
        [DisplayName("Порода")]
        public Breed Breeds { get; set; }

        public int VaccinesId { get; set; }
        [DisplayName("Ваксиниран/а")]
        public Vaccine Vaccines { get; set; }
        
        public int CagesId { get; set; }
        [DisplayName("Клетка")]

        public Cage Cages { get; set; }
        [DisplayName("Снимка")]
        public string ImageURL { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayName("Описание")]
        public string Description { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }
    }
}
