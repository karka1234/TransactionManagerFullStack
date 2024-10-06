using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Models.Services_AccountingAPI;

public class TransactionRequest
{
    [Required]
    [JsonProperty("account_id")]
    public Guid AccountId { get; set; }
    [Required]
    [JsonProperty("amount")]
    public double Amount { get; set; } = 0d;

    public TransactionRequest(Guid accountId, double amount)
    {
        AccountId = accountId;
        Amount = amount;
    }
}
