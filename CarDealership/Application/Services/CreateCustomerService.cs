using Domain.Entities;
using Domain.InProgramData;

namespace Application.Services
{
    public class CreateCustomerService
    {
        private readonly ICustomersRepository _customersRepository;

        public CreateCustomerService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<Customer> CreateCustomerAsync(string name, string phone, string address, HeadFrom headFrom)
        {
            var nextId = await _customersRepository.GetNextAvailableIdAsync();

            var customer = new Customer
            {
                ID = nextId,
                Name = name,
                Phone = phone,
                Address = address,
                HeadFrom = headFrom
            };

            await _customersRepository.AddAsync(customer);
            return customer;
        }
    }
}
