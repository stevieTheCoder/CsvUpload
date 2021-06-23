using CsvUpload.Domain.Accounts;
using NUnit.Framework;

namespace CsvUpload.Domain.UnitTests.Accounts
{
    public class CreateAccountTests
    {
        [Test]
        public void CreateAccountReturnsInstanceWithCorrectValues()
        {
            var first = "Mary";
            var last = "Contrary";

            var account = Account.Create(first, last);
            Assert.AreEqual(first, account.FirstName);
            Assert.AreEqual(last, account.LastName);
        }
    }
}
