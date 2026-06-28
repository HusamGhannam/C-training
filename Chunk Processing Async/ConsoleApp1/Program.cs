public class Program
{
    public async Task ProcessDataChuncksAsync(int chunkNumber)
    {
        Console.WriteLine($"Processing chunk {chunkNumber}");
        await Task.Delay(2000);
        Console.WriteLine($"Finished processing chunk {chunkNumber}");
    }

    public async Task ProcessDataAsync(int totalChunks)
    {
        var tasks = new List<Task>();
        for (int i = 1; i <= totalChunks; i++)
        {
            tasks.Add(ProcessDataChuncksAsync(i));
        }
        await Task.WhenAll(tasks);  
        Console.WriteLine("All chunks processed.");
    }    

    public async static Task Main(string[] args)
    {
        Program program = new Program();
        await program.ProcessDataAsync(5);
    }
}