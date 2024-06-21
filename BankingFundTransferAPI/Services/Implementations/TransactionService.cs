using AutoMapper;
using BankingFundTransferAPI.DTOs;
using BankingFundTransferAPI.Models;
using BankingFundTransferAPI.Repositories.Interfaces;
using BankingFundTransferAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransferResponseDto> TransferFundsAsync(TransferRequestDto transferRequest)
        {
            var sourceAccount = await _accountRepository.GetAccountByNumberAsync(transferRequest.SourceAccountNumber);
            var destinationAccount = await _accountRepository.GetAccountByNumberAsync(transferRequest.DestinationAccountNumber);

            if (sourceAccount == null || destinationAccount == null)
            {
                return new TransferResponseDto { Success = false, Message = "Invalid account number(s)." };
            }

            if (sourceAccount.Balance < transferRequest.Amount)
            {
                return new TransferResponseDto { Success = false, Message = "Insufficient funds." };
            }

            sourceAccount.Balance -= transferRequest.Amount;
            destinationAccount.Balance += transferRequest.Amount;

            var transaction = new Transaction
            {
                SourceAccountId = sourceAccount.Id,
                DestinationAccountId = destinationAccount.Id,
                Amount = transferRequest.Amount,
                TransactionDate = DateTime.Now
            };

            await _transactionRepository.AddTransactionAsync(transaction);
            await _accountRepository.UpdateAccountAsync(sourceAccount);
            await _accountRepository.UpdateAccountAsync(destinationAccount);

            return new TransferResponseDto { Success = true, Message = "Transfer successful." };
        }
    }
}
