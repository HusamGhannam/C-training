using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public record HeadFromStat(string Source, int Count);
    public record SoldCarStat(string Brand, string Model, int Count);
    public record SellerStat(string Name, int CarsSold);

    public class StatisticsService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ICarsRepository _carsRepository;
        private readonly ISellersRepository _sellersRepository;

        public StatisticsService(ICustomersRepository customersRepository, ICarsRepository carsRepository, ISellersRepository sellersRepository)
        {
            _customersRepository = customersRepository;
            _carsRepository = carsRepository;
            _sellersRepository = sellersRepository;
        }

        public async Task<List<HeadFromStat>> GetHeadFromStatsAsync()
        {
            var customers = await _customersRepository.GetAllAsync();
            return customers
                .Where(c => c.HeadFrom != null)
                .GroupBy(c => c.HeadFrom!.ToString()!)
                .Select(g => new HeadFromStat(g.Key, g.Count()))
                .OrderByDescending(s => s.Count)
                .ToList();
        }

        public async Task<List<SoldCarStat>> GetMostSoldCarsAsync()
        {
            var cars = await _carsRepository.GetAllAsync();
            return cars
                .Where(c => c.Status == "Sold")
                .GroupBy(c => new { c.Brand, c.Model })
                .Select(g => new SoldCarStat(g.Key.Brand, g.Key.Model, g.Count()))
                .OrderByDescending(s => s.Count)
                .ToList();
        }

        public async Task<List<SellerStat>> GetTopSellersAsync()
        {
            var sellers = await _sellersRepository.GetAllAsync();
            return sellers
                .OrderByDescending(s => s.CarsSold)
                .Select(s => new SellerStat(s.Name, s.CarsSold))
                .ToList();
        }
    }
}
