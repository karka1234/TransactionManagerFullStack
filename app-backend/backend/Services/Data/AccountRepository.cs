using System;
using System.Linq;
using System.Threading.Tasks;
using backend.DB;
using backend.Models.Services_AccountingAPI;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Data;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateDatabase()
    {
        int affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> AddAccount(Account newAccount)
    {
        _context.Accounts.Add(newAccount);    
        return await UpdateDatabase();    
    }

    public async Task<Account> GetAccountById(Guid id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
    }

    public async Task<bool> CheckIfAccountExist(Guid id)
    {
        return await _context.Accounts.AnyAsync(a => a.AccountId == id);
    }

    public async Task<Account> ChangeAccountBalance_ById(Guid id, double amountToChange)
    {
        Account account = await GetAccountById(id);
        account.Balance += amountToChange;
        return account;
    }

    public async Task<bool> DeleteAccountById(Guid id)
    {
        Account account = await GetAccountById(id);
        _context.Accounts.Remove(account);
        return await UpdateDatabase();
    }    
}
