namespace CatShelter.Data
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public ICollection<Cat> Cats { get; set; }

    }
}
