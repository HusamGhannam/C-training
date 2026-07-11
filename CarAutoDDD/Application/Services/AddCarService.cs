using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class AddCarService
    {
        private readonly ICarsRepository _carRepository;

        public AddCarService(ICarsRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> AddCarAsync(string brand, string model, string color, int year, decimal price)
        {
            var nextId = await _carRepository.GetNextAvailableIdAsync();

            var car = new Car
            {
                ID = nextId,
                Brand = brand,
                Model = model,
                Color = color,
                Year = year,
                Price = price,
                Status = "Available"
            };

            await _carRepository.AddAsync(car);
            return car;
        }
    }
}
