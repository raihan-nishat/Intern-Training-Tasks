class Program
{
    static async Task<string> DownloadFileAsync(string filename)
    {
        Console.WriteLine("Download started...");
        await Task.Delay(2000);
        Console.WriteLine("Download finished.");

    }

    static async Task<string> ProcessFileAsync(string contents)
    {

    }

    static async Task Main()
    {

    }
}