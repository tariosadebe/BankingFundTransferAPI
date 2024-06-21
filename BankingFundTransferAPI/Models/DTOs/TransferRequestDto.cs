namespace BankingFundTransferAPI.DTOs
{
    public class TransferRequestDto
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
