using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.InProgramData;
using InfraStructure.Persistence.Context;

namespace InfraStructure.Persistence.Repositories
{
    public class SellerRepository : ISellersRepository
    {
        private readonly MemoryDbContext _db;
        public SellerRepository(MemoryDbContext db) => _db = db;

        public async Task<IEnumerable<Seller>> GetAllAsync() => await _db.Sellers.AsNoTracking().ToListAsync();

        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _db.Sellers.FindAsync(id);
        }

        public async Task AddAsync(Seller entity)
        {
            _db.Sellers.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller entity)
        {
            _db.Sellers.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Sellers.FindAsync(id);
            if (e is null) return;
            _db.Sellers.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetNextAvailableIdAsync()
        {
            var ids = await _db.Sellers.Select(s => s.ID).ToListAsync();
            return ids.Count == 0 ? 1 : ids.Max() + 1;
        }
    }
}
