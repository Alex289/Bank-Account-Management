using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Controllers
{
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpPost]
        public async Task<Guid> NewBank([FromBody] NewBankViewModel values)
        {
            return await _bankService.CreateNewBankAsync(values);
        }

        [HttpGet]
        public Task<List<BankListViewModel>> GetAll()
        {
            return _bankService.GetAllBanksAsync();
        }

        [HttpGet("charge-interests/{bankId}")]
        public Task<int> ChargeInterests(Guid bankId)
        {
            return _bankService.ChargeInterestsAsync(bankId);
        }
    }
}
