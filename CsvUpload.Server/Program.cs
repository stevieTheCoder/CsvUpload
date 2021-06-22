using CsvUpload.Infrastructure;
using CsvUpload.Infrastructure.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CsvUpload.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    await RunDatabaseMigrations(services);
                    await SeedDatabaseData(services);
                }

                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occured while migrating or seeding the database");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task RunDatabaseMigrations(IServiceProvider services)
        {
            var applicationContext = services.GetRequiredService<ApplicationContext>();
            await applicationContext.Database.MigrateAsync();
        }

        private static async Task SeedDatabaseData(IServiceProvider services)
        {
            var applicationContext = services.GetRequiredService<ApplicationContext>();
            await SeedDatabase.SeedAccountsAsync(applicationContext);
        }
    }
}
