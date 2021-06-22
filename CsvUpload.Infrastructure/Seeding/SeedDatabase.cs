using CsvHelper;
using CsvHelper.Configuration;
using CsvUpload.Application.Interfaces;
using CsvUpload.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvUpload.Infrastructure.Seeding
{
    public static class SeedDatabase
    {
        public class AccountMap : ClassMap<Account>
        {
            public AccountMap()
            {
                Map(a => a.Id).Name("AccountId");
                Map(a => a.FirstName).Name("FirstName");
                Map(a => a.LastName).Name("LastName");
            }
        }

        // Seed database with accounts if none currently exist
        public static async Task SeedAccountsAsync(IApplicationContext context)
        {
            if (await context.Accounts.OrderBy(a => a.Id).FirstOrDefaultAsync() != null)
            {
                return;
            }

            var path = Directory.GetCurrentDirectory() + string.Format("{0}..{0}SeedData{0}Test_Accounts.csv", Path.DirectorySeparatorChar);
            Console.WriteLine(path);

            CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IncludePrivateMembers = true
            };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, configuration))
            {
                
                csv.Context.RegisterClassMap<AccountMap>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var account = csv.GetRecord<Account>();
                    await context.Accounts.AddAsync(account);
                }
            }

            await context.SaveChangesAsync(System.Threading.CancellationToken.None);
        }
    }
}
