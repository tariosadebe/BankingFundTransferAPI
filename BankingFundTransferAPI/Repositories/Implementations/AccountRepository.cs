using BankingFundTransferAPI.Data;
using BankingFundTransferAPI.Models;
using BankingFundTransferAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<Account> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
