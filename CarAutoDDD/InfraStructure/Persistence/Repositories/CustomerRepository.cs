using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.InProgramData;
using InfraStructure.Persistence.Context;

namespace InfraStructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomersRepository
    {
        private readonly MemoryDbContext _db;
        public CustomerRepository(MemoryDbContext db) => _db = db;

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _db.Customers.AsNoTracking().ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) => await _db.Customers.FindAsync(id);

        public async Task AddAsync(Customer entity)
        {
            _db.Customers.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Customers.FindAsync(id);
            if (e is null) return;
            _db.Customers.Remove(e);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetNextAvailableIdAsync()
        {
            var ids = await _db.Customers.Select(c => c.ID).ToListAsync();
            return ids.Count == 0 ? 1 : ids.Max() + 1;
        }
    }
}
