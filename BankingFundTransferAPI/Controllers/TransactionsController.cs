using BankingFundTransferAPI.DTOs;
using BankingFundTransferAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankingFundTransferAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFunds([FromBody] TransferRequestDto transferRequest)
        {
            var result = await _transactionService.TransferFundsAsync(transferRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
