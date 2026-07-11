using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using InfraStructure.Dependencies;
using Presentation;

var services = new ServiceCollection();
services.AddInfrastructure();
services.AddScoped<CarDisplayService>();
services.AddScoped<CustomerDisplayService>();
services.AddScoped<CreateCustomerService>();
services.AddScoped<SellerDisplayService>();
services.AddScoped<SellCarService>();
services.AddScoped<StatisticsService>();
services.AddScoped<AddCarService>();
services.AddScoped<ShowAvailableCarsService>();
services.AddScoped<TestData>();
services.AddScoped<Menu>();

var provider = services.BuildServiceProvider();

var menu = provider.GetRequiredService<Menu>();
await menu.StartAsync();
