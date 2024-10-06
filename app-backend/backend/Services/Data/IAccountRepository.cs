using System;
using System.Threading.Tasks;
using backend.Models.Services_AccountingAPI;

namespace backend.Services.Data;

public interface IAccountRepository
{
    public Task<bool> AddAccount(Account newAccount);
    public Task<Account> GetAccountById(Guid id);
    public Task<Account> ChangeAccountBalance_ById(Guid id, double amountToChange);
    public  Task<bool> CheckIfAccountExist(Guid id);
    public Task<bool> DeleteAccountById(Guid id);
    public  Task<bool> UpdateDatabase();
}
