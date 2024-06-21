using BankingFundTransferAPI.Models;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionByIdAsync(int transactionId);
    }
}
