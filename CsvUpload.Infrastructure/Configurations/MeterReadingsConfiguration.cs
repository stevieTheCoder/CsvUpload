using CsvUpload.Domain.MeterReadings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvUpload.Infrastructure.Configurations
{
    public class MeterReadingsConfiguration : IEntityTypeConfiguration<MeterReading>
    {
        public void Configure(EntityTypeBuilder<MeterReading> builder)
        {
            builder.HasKey(mr => new { mr.Id, mr.ReadingTaken });
        }
    }
}
