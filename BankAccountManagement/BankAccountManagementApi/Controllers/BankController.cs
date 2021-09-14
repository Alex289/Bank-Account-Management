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
    [Route("api/bank")]
    [ApiController]
    public class BankController : ApiController
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService, INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _bankService = bankService;
        }

        [HttpPost]
        public async Task<IActionResult> NewBank([FromBody] NewBankViewModel values)
        {
            var result = await _bankService.CreateNewBankAsync(values);
            return CreateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankService.GetAllBanksAsync();
            return Ok(result);
        }

        [HttpGet("charge-interests/{bankId}")]
        public async Task<IActionResult> ChargeInterests(Guid bankId)
        {
            var result = await _bankService.ChargeInterestsAsync(bankId);
            return CreateResponse(result);
        }
    }
}
