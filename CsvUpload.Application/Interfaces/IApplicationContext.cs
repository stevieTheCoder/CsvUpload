using CsvUpload.Domain.Accounts;
using CsvUpload.Domain.MeterReadings;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CsvUpload.Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<MeterReading> MeterReadings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
