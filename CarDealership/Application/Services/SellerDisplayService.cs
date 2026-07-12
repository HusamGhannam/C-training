using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class SellerDisplayService
    {
        private readonly ISellersRepository _sellersRepository;

        public SellerDisplayService(ISellersRepository sellersRepository)
        {
            _sellersRepository = sellersRepository;
        }

        public async Task<List<Seller>> GetAllSellersAsync()
        {
            var sellers = await _sellersRepository.GetAllAsync();
            return sellers.ToList();
        }

        public async Task DisplaySellersAsync()
        {
            var sellers = await GetAllSellersAsync();
            if (!sellers.Any())
            {
                Console.WriteLine("No sellers available.");
                return;
            }

            Console.WriteLine("ID  Name          City        Cars Sold");
            foreach (var s in sellers)
                Console.WriteLine($"{s.ID,-3} {s.Name,-12} {s.City,-10} {s.CarsSold}");
        }

        public async Task<Seller> CreateSellerAsync(string name, string city)
        {
            var nextId = await _sellersRepository.GetNextAvailableIdAsync();

            var seller = new Seller
            {
                ID = nextId,
                Name = name,
                City = city,
                CarsSold = 0
            };

            await _sellersRepository.AddAsync(seller);
            return seller;
        }
    }
}
