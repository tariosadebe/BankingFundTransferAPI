using BankingFundTransferAPI.DTOs;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransferResponseDto> TransferFundsAsync(TransferRequestDto transferRequest);
    }
}
