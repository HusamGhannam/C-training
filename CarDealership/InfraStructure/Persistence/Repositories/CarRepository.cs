using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.InProgramData;
using InfraStructure.Persistence.Context;

namespace InfraStructure.Persistence.Repositories
{
    public class CarRepository : ICarsRepository
    {
        private readonly MemoryDbContext _db;
        public CarRepository(MemoryDbContext db) => _db = db;

        public async Task<IEnumerable<Car>> GetAllAsync() => await _db.Cars.AsNoTracking().ToListAsync();

        public async Task<Car?> GetByIdAsync(int id) => await _db.Cars.FindAsync(id);

        public async Task AddAsync(Car entity)
        {
            _db.Cars.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car entity)
        {
            _db.Cars.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Cars.FindAsync(id);
            if (e is null) return;
            _db.Cars.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetNextAvailableIdAsync()
        {
            var ids = await _db.Cars.Select(c => c.ID).ToListAsync();
            return ids.Count == 0 ? 1 : ids.Max() + 1;
        }
    }
}
