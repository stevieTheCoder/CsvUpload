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
            var date = DateTime.Now;
            string reading = "01235";
            
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading(date, reading);

            var accountReading = account.MeterReadings.FirstOrDefault();

            Assert.AreEqual(date, accountReading.ReadingTaken);
            Assert.AreEqual(reading, accountReading.Value);
        }


        [TestCase("Z")]
        [TestCase("-123")]
        [TestCase("123456")]
        public void AddMeterReading_DoesNotAddToCollection_WhenInvalidValueFormat(string value)
        { 
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading(DateTime.Now, value);

            Assert.IsEmpty(account.MeterReadings);
        }

        [Test]
        // Unclear whether duplicate reading means same date or same value or both
        // Would need to clarify and adjust tests / logic accordingly
        public void AddMeterReading_DoesNotAddToCollection_WhenAlreadyExists()
        {
            var date = DateTime.Now;
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading(date, "12345");

            // Different reading same date should not add
            account.AddMeterReading(date, "12346");

            Assert.That(account.MeterReadings, Has.Count.EqualTo(1));
        }

        [Test]
        // Unclear whether duplicate reading means same date or same value or both
        // Would need to clarify and adjust tests / logic accordingly
        public void AddMeterReading_DoesAddToCollection_WhenDifferentDates()
        {
            var account = Account.Create("Joe", "Bloggs");
            account.AddMeterReading(DateTime.Now, "12345");

            // Different date should be added
            account.AddMeterReading(DateTime.Now, "12346");

            Assert.That(account.MeterReadings, Has.Count.EqualTo(2));
        }
    }
}
