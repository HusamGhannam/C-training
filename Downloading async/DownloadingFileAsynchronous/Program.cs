public class Program
{
    //identifying the method as asynchronous
    public async Task<string> DownloadFileAsync(string FileName)
    {
        Console.WriteLine("Downloading file: " + FileName);
        await Task.Delay(2000); // Simulate a delay for downloading
        Console.WriteLine("File downloaded: " + FileName);
        return $"{FileName} content.";
    }
    //Using the async method
    public async Task StartingDownloadAsync()
    {
        //we call Download file to download two files asynchronously
        var downloadTask1 = DownloadFileAsync("file1.txt");
        var downloadTask2 = DownloadFileAsync("file2.txt");
        //wait for both downloads to complete
        await Task.WhenAll(downloadTask1, downloadTask2);
        Console.WriteLine("All files downloaded.");
    }




    public static async Task Main(string[] args)
    {
        Program program = new Program();
        //Calling the async method from Main
        await program.StartingDownloadAsync();
    }
}