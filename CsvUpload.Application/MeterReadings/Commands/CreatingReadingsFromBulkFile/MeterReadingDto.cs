using CsvHelper.Configuration.Attributes;

namespace CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile
{
    public class MeterReadingDto
    {
        [Name("AccountId")]
        public int Id { get; set; }
        [Name("MeterReadingDateTime")]
        public string Date { get; set; }
        [Name("MeterReadValue")]
        public string Value { get; set; }
    }
}
