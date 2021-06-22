using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ArgumentException = System.ArgumentException;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using Console = System.Console;
using Environment = System.Environment;

namespace Venture.SharedKernel.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "DefaultConnection";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}CsvUpload.Server", Path.DirectorySeparatorChar);
            return CreateWithConfiguration(Environment.GetEnvironmentVariable(AspNetCoreEnvironment), basePath);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext CreateWithConfiguration(string environmentName, string basePath)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsHistoryTable("__MigrationsHistory", typeof(TContext).Name.Replace("Context", "")));

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}