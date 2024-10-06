using System;
using System.Threading.Tasks;
using backend.Models.Services_AccountingAPI;
using backend.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepo = accountRepository;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount([FromRoute] Guid accountId) 
        {
            if(accountId == Guid.Empty)
                return StatusCode(400, "account_id missing or has incorrect type.");
            if(await _accountRepo.CheckIfAccountExist(accountId))
            {
                Account account = await _accountRepo.GetAccountById(accountId);
                return Ok(account);
            }
            return StatusCode(404,"Account not found.");      
        }

    }
}
