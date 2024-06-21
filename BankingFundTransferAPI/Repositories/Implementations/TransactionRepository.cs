using BankingFundTransferAPI.Data;
using BankingFundTransferAPI.Models;
using BankingFundTransferAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _context;

        public TransactionRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }
    }
}
