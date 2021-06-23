using CsvUpload.Domain.Shared;
using System.Linq;

namespace CsvUpload.Domain.MeterReadings.BusinessRules
{
    public class ReadingValueShouldBeFormattedCorrectly : IBusinessRule
    {
        private readonly string _value;
        private const int RequiredReadingLength = 5;

        public ReadingValueShouldBeFormattedCorrectly(string value)
        {
            _value = value;
        }

        public string Message => "Meter reading formatted incorrectly";

        public bool IsBroken() => !_value.All(char.IsDigit) || _value.Length != RequiredReadingLength;
    }
}
