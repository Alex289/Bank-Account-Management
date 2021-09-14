using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> NewAccount([FromBody] NewAccountViewModel values)
        {
            var result = await _accountService.NewAccountAsync(values);
            return CreateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAccountsAsync();
            return Ok(result);
        }

        [HttpGet("bank/{bankId}")]
        public async Task<IActionResult> GetByBank(Guid bankId)
        {
            var result = await _accountService.GetByBankIdAsync(bankId);
            return Ok(result);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var result = await _accountService.GetByAccountIdAsync(accountId);
            return Ok(result);
        }

        [HttpPost("deposit/{accountId}")]
        public async Task<IActionResult> Deposit(Guid accountId, [FromBody] DepositWithdrawViewModel values)
        {
            var result = await _accountService.DepositAsync(accountId, values);
            return CreateResponse(result);
        }

        [HttpPost("withdraw/{accountId}")]
        public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] DepositWithdrawViewModel values)
        {
            var result = await _accountService.WithdrawAsync(accountId, values);
            return CreateResponse(result);
        }
    }
}
