namespace Domain.Entities
{
    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Available";
        public int? CustomerId { get; set; }
    }
}
