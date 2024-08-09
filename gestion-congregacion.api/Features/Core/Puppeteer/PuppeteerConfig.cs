

using PuppeteerSharp;

public static partial class ServicesConfiguration
{
    public static void AddPuppeteer( this IServiceCollection services)
    {
        var browserFetcher = new BrowserFetcher();
        Console.WriteLine("Downloading browser");
        browserFetcher.DownloadAsync().Wait();
    }

}