namespace Domain.Entities
{
    public class Seller
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int CarsSold { get; set; }
    }
}
