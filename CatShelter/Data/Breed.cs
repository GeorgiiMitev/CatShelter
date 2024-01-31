namespace CatShelter.Data
{
    public class Breed
    {
        public int Id { get; set; }
        public string BreedName { get; set; }
        public ICollection<Cat> Cats { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
