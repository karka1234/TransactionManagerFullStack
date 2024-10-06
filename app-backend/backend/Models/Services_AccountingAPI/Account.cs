using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Models.Services_AccountingAPI;

public class Account
{
    [Key]
    [Required]
    [JsonProperty("account_id")]
    public Guid AccountId { get; set; }

    [Required]
    [JsonProperty("balance")]
    public double Balance { get; set; }
    
    public Account(Guid accountId, double balance)
    {
        AccountId = accountId;
        Balance = balance;
    }
    public Account() {}
}
