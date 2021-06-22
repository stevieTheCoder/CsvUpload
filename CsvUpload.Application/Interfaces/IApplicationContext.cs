using CsvUpload.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CsvUpload.Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<User> Users { get; set; }
        DbSet<MeterReading> MeterReadings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
