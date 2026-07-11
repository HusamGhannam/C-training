using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class CarDisplayService
    {
        private readonly ICarsRepository _carRepository;

        public CarDisplayService(ICarsRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return cars.ToList();
        }

        public async Task DisplayCarsAsync()
        {
            var cars = await GetAllCarsAsync();
            if (!cars.Any())
            {
                Console.WriteLine("No cars in inventory.");
                return;
            }

            Console.WriteLine("ID  Brand     Model      Color     Year   Price      Status     CustID");
            foreach (var c in cars)
                Console.WriteLine($"{c.ID,-3} {c.Brand,-9} {c.Model,-9} {c.Color,-9} {c.Year,-5} {c.Price,8:C}  {c.Status,-10} {(c.CustomerId.HasValue ? c.CustomerId.Value.ToString() : "N/A"),-6}");
        }
    }
}
