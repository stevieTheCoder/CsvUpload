using CsvUpload.Domain.MeterReadings.BusinessRules;
using CsvUpload.Domain.Shared;
using System;

namespace CsvUpload.Domain.MeterReadings
{
    public class MeterReading : Entity
    {
        public int Id { get; private set; }
        public DateTime ReadingTaken { get; private set; }
        public string Value { get; private set; }
        private MeterReading()
        {
            // For EF
        }

        private MeterReading(int id, DateTime readingTaken, string value)
        {
            CheckRule(new ReadingValueShouldBeFormattedCorrectly(value));

            Id = id;
            ReadingTaken = readingTaken;
            Value = value;
        }

        public static MeterReading Create(int id, DateTime readingTaken, string value)
        {     
            return new MeterReading(id, readingTaken, value);
        }
    }
}
