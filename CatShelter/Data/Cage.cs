namespace CatShelter.Data
{
    public class Cage
    {
        public int Id { get; set; }
        public int CageNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public ICollection<Cat> Cats { get; set; }
    }
}
