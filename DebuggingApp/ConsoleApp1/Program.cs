 class Program
{

    public static void Main(string[] args)
    {
        // Calling the asynchronous method
        Task.Run(async () => await PerformAsyncOperation()).Wait();
        Console.WriteLine("Main method completed.");
    }

        public static async Task PerformAsyncOperation()
    {
        try
        {
            Console.WriteLine("Starting async operation...");
            await Task.Delay(2000); // Simulate an asynchronous operation
            Console.WriteLine("Async operation completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed : {ex}");
        }
    }
}
