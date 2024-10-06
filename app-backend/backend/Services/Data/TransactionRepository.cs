using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DB;
using backend.Models.Services_AccountingAPI;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Data;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;
    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    private async Task<bool> UpdateDatabase()
    {
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> AddTransaction(Transaction newTransaction)
    {
        _context.Transactions.Add(newTransaction);    
        return await UpdateDatabase();    
    }
    
    public async Task<bool> AddArrayOfTransactions(List<Transaction> newTransactions)
    {
        _context.Transactions.AddRange(newTransactions);
        return await UpdateDatabase();   
    }

    public async Task<List<Transaction>> GetTransactionsByAccountId(Guid id)
    {
        return await _context.Transactions.Where(t => t.AccountId == id).ToListAsync();
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction> GetTransactionById(Guid id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(a => a.TransactionId == id);
    }

    public async Task<bool> DeleteTransactionById(Guid id)
    {
        Transaction transaction = await GetTransactionById(id);
        _context.Transactions.Remove(transaction);
        return await UpdateDatabase();
    }    
    public async Task<bool> CheckIfTransactionExist(Guid id)
    {
        return await _context.Transactions.AnyAsync(a => a.TransactionId == id);
    }

}
