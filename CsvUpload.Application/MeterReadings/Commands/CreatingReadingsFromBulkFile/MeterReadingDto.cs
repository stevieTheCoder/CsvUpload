using CsvHelper.Configuration.Attributes;
using CsvUpload.Application.TypeConverters;
using System;

namespace CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile
{
    public class MeterReadingDto
    {
        [Name("AccountId")]
        public int Id { get; set; }
        [Name("MeterReadingDateTime")]
        [TypeConverter(typeof(DateConverter))]
        public DateTime Date { get; set; }
        [Name("MeterReadValue")]
        public string Value { get; set; }
    }
}
