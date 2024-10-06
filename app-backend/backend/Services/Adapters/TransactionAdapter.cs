using System;
using System.Collections.Generic;
using backend.Models.Requests;
using backend.Models.Services_AccountingAPI;

namespace backend.Services.Adapters;

public class TransactionAdapter : ITransactionAdapter
{
    public TransactionOut Bind(Transaction transaction)
    {
        return new TransactionOut()
        {
            TransactionId = transaction.TransactionId,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount
        };
    }

    public List<TransactionOut> Bind(ArrayOfTransactions transaction)
    {
        List<TransactionOut> transOutLit = new List<TransactionOut>();
        foreach (Transaction trans in transaction.Transactions)
        {
            transOutLit.Add(Bind(trans));
        }
        return transOutLit;
    }    
}
