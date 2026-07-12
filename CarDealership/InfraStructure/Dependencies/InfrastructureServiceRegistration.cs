using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.InProgramData;
using InfraStructure.Persistence.Context;
using InfraStructure.Persistence.Repositories;

namespace InfraStructure.Dependencies
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MemoryDbContext>(options =>
                options.UseInMemoryDatabase("CarDealership"));

            services.AddScoped<ICarsRepository, CarRepository>();
            services.AddScoped<ICustomersRepository, CustomerRepository>();
            services.AddScoped<ISellersRepository, SellerRepository>();

            return services;
        }
    }
}
