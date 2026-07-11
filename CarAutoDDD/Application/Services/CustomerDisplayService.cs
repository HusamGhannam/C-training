using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class CustomerDisplayService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomerDisplayService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _customersRepository.GetAllAsync();
            return customers.ToList();
        }

        public async Task DisplayCustomersAsync()
        {
            var customers = await GetAllCustomersAsync();
            if (!customers.Any())
            {
                Console.WriteLine("No customers available.");
                return;
            }

            Console.WriteLine("ID  Name          Phone         Address       Source");
            foreach (var c in customers)
                Console.WriteLine($"{c.ID,-3} {c.Name,-12} {c.Phone,-12} {c.Address,-12} {c.HeadFrom?.ToString() ?? "N/A"}");
        }
    }
}
