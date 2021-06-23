using CsvUpload.Domain.MeterReadings;
using CsvUpload.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvUpload.Domain.Accounts
{
    public class Account : Entity
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

        /// <summary>
        /// Add meter reading to account
        /// Will only add if values are valid
        /// Currently catches business rule validations errors but could issue domain event on failure etc
        /// </summary>
        /// <param name="readingTaken">Reading taken date as a string</param>
        /// <param name="value">Meter reading value</param>
        public void AddMeterReading(string readingTakenDate, string value)
        {
            try
            {
                if (DateTime.TryParse(readingTakenDate, out var reading))
                {
                    var meterReading = MeterReading.Create(Id, reading, value);

                    if (!_meterReadings.Select(mr => mr.ReadingTaken).Contains(meterReading.ReadingTaken))
                    {
                        _meterReadings.Add(meterReading);
                    }                    
                }
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
