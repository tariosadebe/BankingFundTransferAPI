﻿namespace BankingFundTransferAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
