using System;

namespace CsvUpload.Domain.MeterReadings
{
    public class MeterReading
    {
        public int Id { get; private set; }
        public DateTime ReadingTaken { get; private set; }
        public int Value { get; private set; }
        private MeterReading()
        {
            // For EF
        }

        private MeterReading(int id, DateTime readingTaken, int value)
        {
            Id = id;
            ReadingTaken = readingTaken;
            Value = value;
        }

        public static MeterReading Create(int id, DateTime readingTaken, int value)
        {
            return new MeterReading(id, readingTaken, value);
        }
    }
}
