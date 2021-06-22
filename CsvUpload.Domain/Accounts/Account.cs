namespace CsvUpload.Domain.Accounts
{
    public class Account
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

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
