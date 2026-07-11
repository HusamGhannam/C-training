namespace Domain.Events
{
    public record CarSoldEvent(int CarId, string Brand, string Model, string Color, int Year, decimal Price, DateTime SoldAt, int SellerId, int CustomerId);
}
