using CsvUpload.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvUpload.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                        b.MigrationsHistoryTable("__MigrationsHistory", "Application");
                    }));

            services.AddScoped<IApplicationContext>(p => p.GetService<ApplicationContext>());

            return services;
        }
    }
}
