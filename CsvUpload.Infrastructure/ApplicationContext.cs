using CsvUpload.Application.Interfaces;
using CsvUpload.Domain.Accounts;
using CsvUpload.Domain.MeterReadings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CsvUpload.Infrastructure
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Application");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
