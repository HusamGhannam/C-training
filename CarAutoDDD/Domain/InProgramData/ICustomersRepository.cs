using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.InProgramData
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<int> GetNextAvailableIdAsync();
    }
}
