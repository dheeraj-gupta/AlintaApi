using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using alintaApi.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace alintaApi
{
    public class Program
    {
        //    public static void Main(string[] args)
        //    {
        //        var host = CreateWebHostBuilder(args).Build();

        //        // Find the service layer within our scope.
        //        using (var scope = host.Services.CreateScope())
        //        {
        //            // Get the instance of CustomersDBContext in our services layer
        //            var services = scope.ServiceProvider;
        //            var context = services.GetRequiredService<CustomersDbContext>();

        //            // Call the DataGenerator to create sample data
        //            DataGenerator.Initialize(services);
        //        }

        //        // Continue to run the application
        //        host.Run();
        //    }

        //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //        WebHost.CreateDefaultBuilder(args)
        //            .UseStartup<Startup>();

        private static string _environmentName;

        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            //read configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: true)
                        .Build();



            webHost.Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, config) =>
                {
                    config.ClearProviders();  //Disabling default integrated logger
                    _environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                })
                .UseStartup<Startup>()
                .Build();
    }
}
