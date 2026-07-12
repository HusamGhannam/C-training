using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class TestData
    {
        private readonly ICarsRepository _carRepo;
        private readonly ICustomersRepository _customerRepo;
        private readonly ISellersRepository _sellerRepo;

        public TestData(ICarsRepository carRepo, ICustomersRepository customerRepo, ISellersRepository sellerRepo)
        {
            _carRepo = carRepo;
            _customerRepo = customerRepo;
            _sellerRepo = sellerRepo;
        }

        public async Task SeedAllAsync()
        {
            await SeedCarsAsync();
            await SeedSellersAsync();
            await SeedCustomersAsync();
        }

        private async Task SeedCarsAsync()
        {
            var cars = await _carRepo.GetAllAsync();
            if (cars.Any()) return;

            await _carRepo.AddAsync(new Car { ID = 1, Brand = "Toyota", Model = "Corolla", Color = "White", Year = 2022, Price = 18000, Status = "Available" });
            await _carRepo.AddAsync(new Car { ID = 2, Brand = "Honda", Model = "Civic", Color = "Black", Year = 2023, Price = 22000, Status = "Available" });
            await _carRepo.AddAsync(new Car { ID = 3, Brand = "Ford", Model = "Focus", Color = "Red", Year = 2021, Price = 15000, Status = "Available" });
        }

        private async Task SeedSellersAsync()
        {
            var sellers = await _sellerRepo.GetAllAsync();
            if (sellers.Any()) return;

            await _sellerRepo.AddAsync(new Seller { ID = 1, Name = "Omar", City = "Damascus", CarsSold = 0 });
            await _sellerRepo.AddAsync(new Seller { ID = 2, Name = "Lara", City = "Aleppo", CarsSold = 0 });
        }

        private async Task SeedCustomersAsync()
        {
            var customers = await _customerRepo.GetAllAsync();
            if (customers.Any()) return;

            await _customerRepo.AddAsync(new Customer { ID = 1, Name = "Khaled", Phone = "0933123456", Address = "Damascus", HeadFrom = HeadFrom.Instagram });
            await _customerRepo.AddAsync(new Customer { ID = 2, Name = "Nadia", Phone = "0944567890", Address = "Homs", HeadFrom = HeadFrom.SocialMedia });
            await _customerRepo.AddAsync(new Customer { ID = 3, Name = "Youssef", Phone = "0955678901", Address = "Lattakia", HeadFrom = HeadFrom.StreetAd });
        }
    }
}
