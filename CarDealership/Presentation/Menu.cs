using Application.Services;
using Domain.Entities;

namespace Presentation
{
    public class Menu
    {
        private readonly CarDisplayService _carDisplayService;
        private readonly CustomerDisplayService _customerDisplayService;
        private readonly CreateCustomerService _createCustomerService;
        private readonly SellerDisplayService _sellerDisplayService;
        private readonly SellCarService _sellCarService;
        private readonly StatisticsService _statisticsService;
        private readonly AddCarService _addCarService;
        private readonly ShowAvailableCarsService _showAvailableCarsService;
        private readonly TestData _myData;

        public Menu(
            CarDisplayService carDisplayService,
            CustomerDisplayService customerDisplayService,
            CreateCustomerService createCustomerService,
            SellerDisplayService sellerDisplayService,
            SellCarService sellCarService,
            StatisticsService statisticsService,
            AddCarService addCarService,
            ShowAvailableCarsService showAvailableCarsService,
            TestData myData)
        {
            _carDisplayService = carDisplayService;
            _customerDisplayService = customerDisplayService;
            _createCustomerService = createCustomerService;
            _sellerDisplayService = sellerDisplayService;
            _sellCarService = sellCarService;
            _statisticsService = statisticsService;
            _addCarService = addCarService;
            _showAvailableCarsService = showAvailableCarsService;
            _myData = myData;
        }

        public async Task StartAsync()
        {
            await TestDataAsync();

            while (true)
            {
                Console.WriteLine("===== Car Dealership =====");
                Console.WriteLine("1. Display all cars");
                Console.WriteLine("2. Sell a car");
                Console.WriteLine("3. View sold cars");
                Console.WriteLine("4. View sale history");
                Console.WriteLine("5. View statistics");
                Console.WriteLine("6. View customers");
                Console.WriteLine("7. View sellers");
                Console.WriteLine("8. Add a car");
                Console.WriteLine("9. View available cars");
                Console.WriteLine("10. Add a seller");
                Console.WriteLine("11. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": await _carDisplayService.DisplayCarsAsync(); break;
                    case "2": await SellCarAsync(); break;
                    case "3": await DisplaySoldCarsAsync(); break;
                    case "4": DisplaySalesHistory(); break;
                    case "5": await DisplayStatisticsAsync(); break;
                    case "6": await _customerDisplayService.DisplayCustomersAsync(); break;
                    case "7": await _sellerDisplayService.DisplaySellersAsync(); break;
                    case "8": await AddCarAsync(); break;
                    case "9": await _showAvailableCarsService.DisplayAvailableCarsAsync(); break;
                    case "10": await AddSellerAsync(); break;
                    case "11": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
                Console.WriteLine();
            }
        }

        private async Task TestDataAsync()
        {
            await _myData.AddTestDataAsync();
            Console.WriteLine("Test data added.\n");
        }

        private async Task SellCarAsync()
        {
            Console.Write("Enter car ID to sell: ");
            if (!int.TryParse(Console.ReadLine(), out var carId))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var sellers = await _sellerDisplayService.GetAllSellersAsync();
            if (!sellers.Any())
            {
                Console.WriteLine("No sellers available.");
                return;
            }

            Console.WriteLine("Available sellers:");
            foreach (var s in sellers)
                Console.WriteLine($"  {s.ID}. {s.Name} ({s.City}) - Cars sold: {s.CarsSold}");

            Console.Write("Enter seller ID: ");
            if (!int.TryParse(Console.ReadLine(), out var sellerId))
            {
                Console.WriteLine("Invalid seller ID.");
                return;
            }

            Console.WriteLine("Is the customer old or new?");
            Console.WriteLine("1. Old customer");
            Console.WriteLine("2. New customer");
            Console.Write("Choose: ");

            int customerId;
            switch (Console.ReadLine())
            {
                case "1":
                    var customers = await _customerDisplayService.GetAllCustomersAsync();
                    if (!customers.Any())
                    {
                        Console.WriteLine("No customers available.");
                        return;
                    }

                    Console.WriteLine("Available customers:");
                    foreach (var c in customers)
                        Console.WriteLine($"  {c.ID}. {c.Name} ({c.Phone})");

                    Console.Write("Enter customer ID: ");
                    if (!int.TryParse(Console.ReadLine(), out customerId))
                    {
                        Console.WriteLine("Invalid customer ID.");
                        return;
                    }
                    break;

                case "2":
                    Console.Write("Enter name: ");
                    var name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Name cannot be empty.");
                        return;
                    }

                    Console.Write("Enter phone: ");
                    var phone = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        Console.WriteLine("Phone cannot be empty.");
                        return;
                    }

                    Console.Write("Enter address: ");
                    var address = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(address))
                    {
                        Console.WriteLine("Address cannot be empty.");
                        return;
                    }

                    Console.WriteLine("How did they find us?");
                    Console.WriteLine("  1. Instagram");
                    Console.WriteLine("  2. Facebook");
                    Console.WriteLine("  3. SocialMedia");
                    Console.WriteLine("  4. SomeoneToldMe");
                    Console.WriteLine("  5. StreetAd");

                    HeadFrom headFrom;
                    while (true)
                    {
                        Console.Write("Choose: ");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case "1": headFrom = HeadFrom.Instagram; goto done;
                            case "2": headFrom = HeadFrom.Facebook; goto done;
                            case "3": headFrom = HeadFrom.SocialMedia; goto done;
                            case "4": headFrom = HeadFrom.SomeoneToldMe; goto done;
                            case "5": headFrom = HeadFrom.StreetAd; goto done;
                            default: Console.WriteLine("Invalid choice. Please enter 1-5."); break;
                        }
                    }
                    done:

                    var newCustomer = await _createCustomerService.CreateCustomerAsync(name, phone, address, headFrom);
                    customerId = newCustomer.ID;
                    Console.WriteLine($"New customer created with ID: {customerId}");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            try
            {
                var ev = await _sellCarService.SellCarAsync(carId, sellerId, customerId);
                Console.WriteLine($"Sold: {ev.Brand} {ev.Model} {ev.Color} ({ev.Year}) for {ev.Price:C} by seller #{ev.SellerId} to customer #{ev.CustomerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task AddCarAsync()
        {
            Console.Write("Enter brand: ");
            var brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand))
            {
                Console.WriteLine("Brand cannot be empty.");
                return;
            }

            Console.Write("Enter model: ");
            var model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model))
            {
                Console.WriteLine("Model cannot be empty.");
                return;
            }

            Console.Write("Enter color: ");
            var color = Console.ReadLine();
            if (color == null || color == "")
            {
                Console.WriteLine("Color cannot be empty.");
                return;
            }

            Console.Write("Enter year: ");
            
            if (!int.TryParse(Console.ReadLine(), out var year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }

            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            try
            {
                var car = await _addCarService.AddCarAsync(brand, model, color, year, price);
                Console.WriteLine("Car added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task AddSellerAsync()
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter city: ");
            var city = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("City cannot be empty.");
                return;
            }

            try
            {
                var seller = await _sellerDisplayService.CreateSellerAsync(name, city);
                Console.WriteLine($"Seller added successfully with ID: {seller.ID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task DisplaySoldCarsAsync()
        {
            var soldCars = await _sellCarService.GetSoldCarsAsync();
            if (!soldCars.Any())
            {
                Console.WriteLine("No cars have been sold yet.");
                return;
            }

            Console.WriteLine("ID  Brand     Model      Color     Year   Price      Status");
            foreach (var c in soldCars)
                Console.WriteLine($"{c.ID,-3} {c.Brand,-9} {c.Model,-9} {c.Color,-9} {c.Year,-5} {c.Price,8:C}  {c.Status}");
        }

        private void DisplaySalesHistory()
        {
            var events = _sellCarService.Events;
            if (events.Count == 0)
            {
                Console.WriteLine("No sales recorded.");
                return;
            }

            Console.WriteLine("Date                Brand     Model      Color     Year   Price      SellerID  CustID");
            foreach (var e in events)
                Console.WriteLine($"{e.SoldAt,-19:yyyy-MM-dd HH:mm:ss} {e.Brand,-9} {e.Model,-9} {e.Color,-9} {e.Year,-5} {e.Price,8:C}  {e.SellerId,-8}  {e.CustomerId}");
        }

        private async Task DisplayStatisticsAsync()
        {
            Console.WriteLine("===== Statistics =====\n");

            var headFromStats = await _statisticsService.GetHeadFromStatsAsync();
            Console.WriteLine("===Customers by Marketing Source ===");
            if (headFromStats.Count == 0)
            {
                Console.WriteLine("  No customer data available.");
            }
            else
            {
                foreach (var s in headFromStats)
                    Console.WriteLine($"  {s.Source,-15} {s.Count}");
            }

            Console.WriteLine("\n===Most Sold Cars ===");
            var stats2 = await _statisticsService.GetMostSoldCarsAsync();
            if (stats2.Count == 0)
            {
                Console.WriteLine("  No sales data available.");
            }
            else
            {
                foreach (var x in stats2)
                    Console.WriteLine($"  {x.Brand} {x.Model,-9} {x.Count} sold");
            }

            Console.WriteLine("\n=== Top Sellers ===");
            var sellerStats = await _statisticsService.GetTopSellersAsync();
            if (sellerStats.Count == 0)
            {
                Console.WriteLine("  No seller data available.");
            }
            else
            {
                foreach (var s in sellerStats)
                    Console.WriteLine($"  {s.Name},  {s.CarsSold} cars sold");
            }
        }
    }
}
