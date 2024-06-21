using BankingFundTransferAPI.Models;
using System.Linq;

namespace BankingFundTransferAPI.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(BankingDbContext context)
        {
            if (context.Accounts.Any() || context.Transactions.Any())
            {
                return; // DB has been seeded
            }

            var accounts = new Account[]
            {
                new Account { AccountNumber = "123456789", Balance = 1000, AccountHolderName = "John Doe" },
                new Account { AccountNumber = "987654321", Balance = 2000, AccountHolderName = "Jane Smith" }
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();
        }
    }
}
