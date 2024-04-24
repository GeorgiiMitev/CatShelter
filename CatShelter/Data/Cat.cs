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
        [DisplayName("Порода")]
        public int BreedsId { get; set; }

        public Breed Breeds { get; set; }
        [DisplayName("Ваксина")]
        public int VaccinesId { get; set; }

        public Vaccine Vaccines { get; set; }
        [DisplayName("Клетка")]
        public int CagesId { get; set; }


        public Cage Cages { get; set; }
        [DisplayName("Снимка URL")]
        public string ImageURL { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayName("Описание")]
        public string Description { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }
    }
}
