using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using backend.Models.Requests;
using backend.Models.Services_AccountingAPI;
using backend.Services.Adapters;
using backend.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transRepo;
        private readonly ITransactionAdapter _transAdapter;
        public TransactionsController(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ITransactionAdapter transactionAdapter)
        {
            _accountRepo = accountRepository;
            _transRepo = transactionRepository;
            _transAdapter = transactionAdapter;
        }

        [HttpPost]
        public async Task<IActionResult> PostTransaction([FromBody] TransactionRequest tR)
        {           
            if (tR == null || tR.AccountId == Guid.Empty || tR.Amount == 0.0)
                return StatusCode(400, "Mandatory body parameters missing or have incorrect type.");        
            if(await _accountRepo.CheckIfAccountExist(tR.AccountId))
            {
                Account account = await _accountRepo.ChangeAccountBalance_ById(tR.AccountId, tR.Amount);
                if(!(await _accountRepo.UpdateDatabase()))
                    return StatusCode(415, "Specified content type not allowed.");
            }
            else
            {
                Account account = new Account(tR.AccountId, tR.Amount);
                if(!(await _accountRepo.AddAccount(account)))
                    return StatusCode(415, "Specified content type not allowed.");
            }      
            Transaction newTransaction = new Transaction(tR.AccountId, tR.Amount);    
            if(await _transRepo.AddTransaction(newTransaction))            
                return StatusCode(201, newTransaction);          
            return StatusCode(400);           
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] Guid transactionId) 
        {
            if(transactionId == Guid.Empty)
                return StatusCode(400,"transaction_id missing or has incorrect type.");
            if(!await _transRepo.CheckIfTransactionExist(transactionId))
                return StatusCode(404, "No transactions found.");
            Transaction trans = await _transRepo.GetTransactionById(transactionId);
            TransactionOut tOut = _transAdapter.Bind(trans);//new TransactionOut(trans.TransactionId, trans.AccountId, trans.Amount);
            return StatusCode(200, tOut);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions() //need to test
        {
            ArrayOfTransactions transactions = new ArrayOfTransactions();
            transactions.Transactions = await _transRepo.GetAllTransactions();               
            if (transactions.Transactions == null || !transactions.Transactions.Any())
                return StatusCode(404, "No transactions found.");
            var transOutList = _transAdapter.Bind(transactions);
            return StatusCode(200, transOutList);
        }
    }
}
