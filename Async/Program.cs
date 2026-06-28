public class Program
{
    public async Task DownloadFileAsync(string url)
    {
        try{
       Console.WriteLine($"Starting download from {url}...");
       throw new Exception("Simulated download error.");
       await Task.Delay(2000); 
       Console.WriteLine($"Download from {url} completed."); 
        }
    catch(Exception ex)
        {
            Console.WriteLine($"Error downloading from {url}: {ex.Message}");
        }
    }
    public async Task DownloadFilesAsync2(string url)
    {
        Console.WriteLine($"Starting download from {url}...");
        await Task.Delay(1000); 
        Console.WriteLine($"Download from {url} completed."); 
    }
    public static async Task Main(string[] args)
    {

        Program program = new Program();
        Task task1 = program.DownloadFileAsync("https://example.com/file1");
        Task task2 = program.DownloadFilesAsync2("https://example.com/file2");
        await Task.WhenAll(task1, task2);
        Console.WriteLine("All downloads completed, Main Done.");
    }
    
}