namespace CatShelter.Data
{
    public class Adoption
    {
        public int Id { get; set; }
        public string ClientsId { get; set; }
        public Client Clients { get; set; }
        public int CatsId { get; set; }
        public Cat Cats { get; set; }
        public string Description { get; set; }
        public DateTime AdoptionDate { get; set; } = DateTime.Now;
    }
}
