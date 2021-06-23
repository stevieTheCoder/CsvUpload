using CsvUpload.Domain.Accounts;
using NUnit.Framework;
using System;
using System.Linq;

namespace CsvUpload.Domain.UnitTests.Accounts
{
    public class AddMeterReadingTests
    {
        [Test]
        public void AddMeterReading_AddsToCollection_WhenValid()
        {
            string date = "12/05/2021";
            string reading = "01235";
            
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading(date, reading);

            var accountReading = account.MeterReadings.FirstOrDefault();

            Assert.AreEqual(DateTime.Parse(date), accountReading.ReadingTaken);
            Assert.AreEqual(reading, accountReading.Value);
        }

        [Test]
        public void AddMeterReading_DoesNotAddToCollection_WhenInvalidDate()
        {
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading("12/05/XXX", "12345");

            Assert.IsEmpty(account.MeterReadings);
        }

        [TestCase("Z")]
        [TestCase("-123")]
        [TestCase("123456")]
        public void AddMeterReading_DoesNotAddToCollection_WhenInvalidValueFormat(string value)
        { 
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading("12/05/2021", value);

            Assert.IsEmpty(account.MeterReadings);
        }

        [Test]
        // Unclear whether duplicate reading means same date or same value or both
        // Would need to clarify and adjust tests / logic accordingly
        public void AddMeterReading_DoesNotAddToCollection_WhenAlreadyExists()
        {            
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading("12/05/2021", "12345");

            // Different reading same date should not add
            account.AddMeterReading("12/05/2021", "12346");

            Assert.That(account.MeterReadings, Has.Count.EqualTo(1));
        }

        [Test]
        // Unclear whether duplicate reading means same date or same value or both
        // Would need to clarify and adjust tests / logic accordingly
        public void AddMeterReading_DoesAddToCollection_WhenDifferentDates()
        {
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading("12/05/2021", "12345");

            // Different date should be added
            account.AddMeterReading("13/05/2021", "12346");

            Assert.That(account.MeterReadings, Has.Count.EqualTo(2));
        }
    }
}
