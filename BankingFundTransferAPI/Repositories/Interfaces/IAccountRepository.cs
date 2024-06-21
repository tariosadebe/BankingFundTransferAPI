using BankingFundTransferAPI.Models;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<Account> GetAccountByNumberAsync(string accountNumber);
        Task UpdateAccountAsync(Account account);
    }
}
