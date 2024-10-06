using System;
using System.Collections.Generic;
using backend.Models.Requests;
using backend.Models.Services_AccountingAPI;

namespace backend.Services.Adapters;

public interface ITransactionAdapter
{
    public TransactionOut Bind(Transaction transaction);
    public List<TransactionOut> Bind(ArrayOfTransactions transaction);
}
