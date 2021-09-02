using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public Task<Guid> NewAccount([FromBody] NewAccountViewModel values)
        {
            return _accountService.NewAccountAsync(values);
        }

        [HttpGet]
        public Task<List<AccountListViewModel>> GetAll()
        {
            return _accountService.GetAllAccountsAsync();
        }

        [HttpGet("bank/{bankId}")]
        public Task<List<AccountListViewModel>> GetByBank(Guid bankId)
        {
            return _accountService.GetByBankIdAsync(bankId);
        }

        [HttpGet("{accountId}")]
        public Task<AccountListViewModel> GetAccount(Guid accountId)
        {
            return _accountService.GetByAccountIdAsync(accountId);
        }

        [HttpPost("deposit/{accountId}")]
        public Task<decimal> Deposit(Guid accountId, [FromBody] DepositWithdrawViewModel values)
        {
            return _accountService.DepositAsync(accountId, values);
        }

        [HttpPost("withdraw/{accountId}")]
        public Task<decimal> Withdraw(Guid accountId, [FromBody] DepositWithdrawViewModel values)
        {
            return _accountService.WithdrawAsync(accountId, values);
        }
    }
}
