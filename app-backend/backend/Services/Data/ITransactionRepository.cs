using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models.Services_AccountingAPI;

namespace backend.Services.Data;

public interface ITransactionRepository
{
    public  Task<bool> AddTransaction(Transaction newTransaction);
    
    public  Task<bool> AddArrayOfTransactions(List<Transaction> newTransactions);

    public  Task<List<Transaction>> GetTransactionsByAccountId(Guid id);

    public  Task<Transaction> GetTransactionById(Guid id);

    public  Task<bool> DeleteTransactionById(Guid id);
    public  Task<bool> CheckIfTransactionExist(Guid id);
    public Task<List<Transaction>> GetAllTransactions();
}
