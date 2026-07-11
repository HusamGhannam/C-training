using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.InProgramData
{
    public interface ISellersRepository
    {
        Task<IEnumerable<Seller>> GetAllAsync();
        Task<Seller?> GetByIdAsync(int id);
        Task AddAsync(Seller seller);
        Task UpdateAsync(Seller seller);
        Task DeleteAsync(int id);
        Task<int> GetNextAvailableIdAsync();
    }
}
