using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace InfraStructure.Persistence.Context
{
    public class MemoryDbContext : DbContext
    {
        public MemoryDbContext(DbContextOptions<MemoryDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Seller> Sellers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.HeadFrom)
                    .HasConversion<string>();
            });
        }
    }
}
