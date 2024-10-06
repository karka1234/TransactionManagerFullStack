using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace backend.Models.Services_AccountingAPI;

public class Transaction
{
    [Key]
    [Required]
    [JsonProperty("transaction_id")]
    public Guid TransactionId { get; set;}
    
    [Required]
    [JsonProperty("account_id")]
    public Guid AccountId { get; set;}

    [Required]
    [JsonProperty("amonut")]
    public double Amount { get; set;}

    [Required]
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set;}

    public Transaction(){}

    public Transaction(Guid accountId, double amount)
    {
        TransactionId= Guid.NewGuid();
        AccountId=accountId;
        Amount=amount;
        CreatedAt=DateTime.Now;
    }

}
