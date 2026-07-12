using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.InProgramData
{
    public interface ICarsRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
        Task<int> GetNextAvailableIdAsync();
    }
}
