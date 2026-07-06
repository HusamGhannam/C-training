using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

interface IDisplay
{
    void Display();
}

class Program
{
    public static List<Car> Cars = new();
    public static List<Customer> Customers = new();
    public static List<Seller> Sellers = new();

    static void Main()
    {
        // Test the application so far
        Program.Cars.Add(new Car(1, 2022, "Toyota", "Corolla", 18000) { Quantity = 10 });
        Program.Cars.Add(new Car(2, 2023, "Honda", "Civic", 22000) { Quantity = 5 });
        Program.Cars.Add(new Car(3, 2021, "Ford", "Focus", 15000) { Quantity = 8 });
        Program.Customers.Add(new Customer(1, "Alice", 28, "Social Media"));
        Program.Customers.Add(new Customer(2, "Bob", 35, "From someone"));
        Program.Customers.Add(new Customer(3, "Charlie", 42, "Instagram"));
        Program.Sellers.Add(new Seller(1, "John Smith", "Sales"));
        Program.Sellers.Add(new Seller(2, "Jane Doe", "Sales"));

        Console.WriteLine("Test data added successfully.\n");
        Menu.Start();
    }
    public class Helper
    {
        public static int GetNextAvailableId(List<int> ids)
        {
            for (int i = 1; ; i++)
            {
                if (!ids.Contains(i))
                {
                    return i;
                }
            }
        }

        public static int GetNextCarId()
        {
            return GetNextAvailableId(Program.Cars.ConvertAll(c => c.Id));
        }

        public static int GetNextCustomerId()
        {
            return GetNextAvailableId(Program.Customers.ConvertAll(c => c.Id));
        }

        public static int GetNextSellerId()
        {
            return GetNextAvailableId(Program.Sellers.ConvertAll(c => c.Id));
        }

        public static void SellCar(int id)
        {
            Program.Cars.RemoveAll(c => c.Id == id);
        }

        // Fixed: Changed from runtime type switch on object for type safety
        public static void DisplayList<T>(List<T> list) where T : IDisplay
        {
            foreach (var item in list)
            {
                item.Display();
            }
        }
    }


    public class Car : IDisplay
    {
        public int Id { get; set; }
        public int ModelYear { get; set; }
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public decimal Price { get; set; }
        public int SoldCars { get; set; }
        public int Quantity { get; set; }

        public Car(int id, int modelYear, string brand, string model, decimal price)
        {
            Id = id;
            ModelYear = modelYear;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public static Car CreateCar()
        {
            int id = Program.Helper.GetNextCarId();

            Console.Write("Enter model year: ");
            int modelYear = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter brand: ");
            string brand = Console.ReadLine() ?? "";

            Console.Write("Enter model: ");
            string model = Console.ReadLine() ?? "";

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            return new Car(id, modelYear, brand, model, price);
        }

        public decimal CalculateSales()
        {
            return SoldCars * Price;
        }

        public static void AddCars()
        {
            Console.Write("Enter car ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var car = Program.Cars.Find(c => c.Id == id);
            if (car == null)
            {
                Console.WriteLine("Car not found.");
                return;
            }

            Console.Write("Enter quantity to add: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            car.Quantity += quantity;
            Console.WriteLine($"Added {quantity} to {car.Brand} {car.Model}. New stock: {car.Quantity}");
        }

        public void Display()
        {
            Console.WriteLine($"Car ID: {Id}, Model Year: {ModelYear}, Brand: {Brand}, Model: {Model}, Price: {Price}, Sold: {SoldCars}, In Stock: {Quantity}");
        }
    }

    public class Customer : IDisplay
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string HeadFrom { get; set; } = "";

        public Customer(int id, string name, int age, string headFrom)
        {
            Id = id;
            Name = name;
            Age = age;
            HeadFrom = headFrom;
        }

        
        public static Customer CreateCustomer()
        {
            int id = Program.Helper.GetNextCustomerId();

            Console.Write("Enter customer name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter customer age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            string headFrom = Survey.Start();

            return new Customer(id, name, age, headFrom);
        }

        public void Display()
        {
            Console.WriteLine($"Customer ID: {Id}, Name: {Name}, Age: {Age}, Heard From: {HeadFrom}");
        }
    }

    public class Seller : IDisplay
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Department { get; set; } = "";
        public int CarsSold { get; set; }

        public Seller(int id, string fullName, string department)
        {
            Id = id;
            FullName = fullName;
            Department = department;
        }

        
        public static Seller CreateSeller()
        {
            int id = Program.Helper.GetNextSellerId();

            Console.Write("Enter seller full name: ");
            string fullName = Console.ReadLine() ?? "";

            Console.Write("Enter seller department: ");
            string department = Console.ReadLine() ?? "";

            return new Seller(id, fullName, department);
        }

        public void Display()
        {
            Console.WriteLine($"Seller ID: {Id}, Full Name: {FullName}, Department: {Department}, Cars Sold: {CarsSold}");
        }
    }
    
}

static class Survey
{
    public static string Start()
    {
        while (true)
        {
            Console.WriteLine("===== How did the customer hear about us? =====");
            Console.WriteLine("1. Instagram");
            Console.WriteLine("2. Facebook");
            Console.WriteLine("3. Other social media platform");
            Console.WriteLine("4. From someone");
            Console.WriteLine("5. Road advertisement");
            Console.WriteLine("6. Other");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return "Instagram";
                case "2":
                    return "Facebook";
                case "3":
                    return "Other social media platform";
                case "4":
                    return "From someone";
                case "5":
                    return "Road advertisement";
                case "6":
                    return "Other";
                default:
                    Console.WriteLine("Invalid option. Please try again.\n");
                    break;
            }
        }
    }
}

static class Menu
{
    public static void Start()
    {
        while (true)
        {
            Console.WriteLine("===== Main Menu =====");
            Console.WriteLine("1. Display");
            Console.WriteLine("2. Transaction");
            Console.WriteLine("3. Statistics");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayMenu();
                    break;
                case "2":
                    TransactionMenu();
                    break;
                case "3":
                    StatisticsMenu();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Display Menu =====");
            Console.WriteLine("1. Display cars");
            Console.WriteLine("2. Display customers");
            Console.WriteLine("3. Display sellers");
            Console.WriteLine("4. Back");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Program.Helper.DisplayList(Program.Cars);
                    break;
                case "2":
                    Program.Helper.DisplayList(Program.Customers);
                    break;
                case "3":
                    Program.Helper.DisplayList(Program.Sellers);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void TransactionMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Transaction Menu =====");
            Console.WriteLine("1. Sell a car");
            Console.WriteLine("2. Return a car");
            Console.WriteLine("3. Add cars");
            Console.WriteLine("4. Back");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SellCar();
                    break;
                case "2":
                    ReturnCar();
                    break;
                case "3":
                    Program.Car.AddCars();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void StatisticsMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Statistics Menu =====");
            Console.WriteLine("1. Cars sold");
            Console.WriteLine("2. Best sellers");
            Console.WriteLine("3. Most forwarded");
            Console.WriteLine("4. Back");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowCarsSold();
                    break;
                case "2":
                    ShowBestSellers();
                    break;
                case "3":
                    ShowMostForwarded();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void SellCar()
    {
        Console.Write("Enter car brand: ");
        string carBrand = Console.ReadLine() ?? "";

        var car = Program.Cars.Find(c =>
            c.Brand.Equals(carBrand, StringComparison.OrdinalIgnoreCase));

        if (car == null)
        {
            Console.WriteLine("Car not found.");
            return;
        }

        Console.Write("Enter seller name: ");
        string sellerName = Console.ReadLine() ?? "";

        var seller = Program.Sellers.Find(s =>
            s.FullName.Equals(sellerName, StringComparison.OrdinalIgnoreCase));

        if (seller == null)
        {
            Console.WriteLine("Seller not found.");
            return;
        }

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (car.Quantity < quantity)
        {
            Console.WriteLine($"Not enough stock. Available: {car.Quantity}");
            return;
        }

        car.Quantity -= quantity;
        car.SoldCars += quantity;
        seller.CarsSold += quantity;

        Console.WriteLine($"Sold {quantity} {car.Brand} {car.Model}(s).");
    }

    static void ReturnCar()
    {
        Console.Write("Enter car brand to return: ");
        string carBrand = Console.ReadLine() ?? "";

        var car = Program.Cars.Find(c =>
            c.Brand.Equals(carBrand, StringComparison.OrdinalIgnoreCase));

        if (car == null)
        {
            Console.WriteLine("Car not found.");
            return;
        }

        Console.Write("Enter quantity to return: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (car.SoldCars < quantity)
        {
            Console.WriteLine($"Cannot return more than sold. Sold: {car.SoldCars}");
            return;
        }

        car.Quantity += quantity;
        car.SoldCars -= quantity;

        Console.WriteLine($"Returned {quantity} {car.Brand} {car.Model}(s).");
    }

    static void ShowCarsSold()
    {
        var sorted = Program.Cars.OrderByDescending(c => c.SoldCars).ToList();

        Console.WriteLine("===== Cars Sold =====");
        foreach (var car in sorted)
        {
            Console.WriteLine($"{car.Brand} {car.Model}: {car.SoldCars} sold");
        }
    }

    static void ShowBestSellers()
    {
        var sorted = Program.Sellers.OrderByDescending(s => s.CarsSold).ToList();

        Console.WriteLine("===== Best Sellers =====");
        foreach (var seller in sorted)
        {
            Console.WriteLine($"{seller.FullName}: {seller.CarsSold} cars sold");
        }
    }

    static void ShowMostForwarded()
    {
        if (Program.Customers.Count == 0)
        {
            Console.WriteLine("No customer data available.");
            return;
        }

        var groups = Program.Customers
            .GroupBy(c => c.HeadFrom)
            .OrderByDescending(g => g.Count())
            .ToList();

        int total = Program.Customers.Count;

        Console.WriteLine("===== Most Forwarded =====");
        foreach (var group in groups)
        {
            double percentage = (double)group.Count() / total * 100;
            Console.WriteLine($"{group.Key}: {group.Count()} ({percentage:F1}%)");
        }
    }
}