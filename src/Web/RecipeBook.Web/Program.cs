namespace RecipeBook.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System.IO;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //.UseContentRoot(Directory.GetCurrentDirectory())
                    //.UseIISIntegration()
                    .UseStartup<Startup>();

                    // .UseUrls("http://localhost:5000", "https://localhost:5001", "https://RecBook:5001", "https://192.168.50.109:5001");
                    });
    }
}
