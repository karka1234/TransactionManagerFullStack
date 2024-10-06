using System;
using Newtonsoft.Json;

namespace backend.Models.Requests;

public class TransactionOut
{
    [JsonProperty("transaction_id")]
    public Guid TransactionId { get; set;}

    [JsonProperty("account_id")]
    public Guid AccountId { get; set; }

    [JsonProperty("amount")]
    public double Amount { get; set; } = 0d;

    public TransactionOut(Guid transactionId, Guid accountId, double amount)
    {
        TransactionId = transactionId;
        AccountId = accountId;
        Amount = amount;
    }

    public TransactionOut(){}
}
