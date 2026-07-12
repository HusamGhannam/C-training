using Domain.Entities;
using Domain.Events;
using Domain.InProgramData;

namespace Application.Services
{
    public class SellCarService
    {
        private readonly ICarsRepository _carRepository;
        private readonly ISellersRepository _sellersRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly List<CarSoldEvent> _events = [];

        public IReadOnlyList<CarSoldEvent> Events => _events.AsReadOnly();

        public SellCarService(ICarsRepository carRepository, ISellersRepository sellersRepository, ICustomersRepository customersRepository)
        {
            _carRepository = carRepository;
            _sellersRepository = sellersRepository;
            _customersRepository = customersRepository;
        }

        // TODO: move this query into the repository instead of filtering in memory
        public async Task<List<Car>> GetSoldCarsAsync()
        {
            var allCars = await _carRepository.GetAllAsync();
            return allCars.Where(c => c.Status == "Sold").ToList();
        }

        public async Task<CarSoldEvent> SellCarAsync(int carId, int sellerId, int customerId)
        {
            var car = await _carRepository.GetByIdAsync(carId)
                ?? throw new InvalidOperationException($"Car with ID {carId} not found.");

            if (car.Status == "Sold")
                throw new InvalidOperationException($"Car with ID {carId} is already sold.");

            var seller = await _sellersRepository.GetByIdAsync(sellerId)
                ?? throw new InvalidOperationException($"Seller with ID {sellerId} not found.");

            var customer = await _customersRepository.GetByIdAsync(customerId)
                ?? throw new InvalidOperationException($"Customer with ID {customerId} not found.");

            car.Status = "Sold";
            car.CustomerId = customerId;
            await _carRepository.UpdateAsync(car);

            seller.CarsSold++;
            await _sellersRepository.UpdateAsync(seller);

            var ev = new CarSoldEvent(car.ID, car.Brand, car.Model, car.Color, car.Year, car.Price, DateTime.UtcNow, sellerId, customerId);
            _events.Add(ev);

            return ev;
        }
    }
}
