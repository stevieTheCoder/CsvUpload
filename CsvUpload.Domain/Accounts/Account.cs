using CsvUpload.Domain.MeterReadings;
using System.Collections.Generic;

namespace CsvUpload.Domain.Accounts
{
    public class Account
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private readonly List<MeterReading> _meterReadings = new List<MeterReading>();
        public IEnumerable<MeterReading> MeterReadings => _meterReadings.AsReadOnly();

        private Account()
        {
            // For EF
        }

        private Account(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Account Create(string firstName, string lastName)
        {
            return new Account(firstName, lastName);
        }
    }
}
