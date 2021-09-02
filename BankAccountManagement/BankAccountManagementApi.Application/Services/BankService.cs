using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.Queries.Bank;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Commands.Bank;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.Services
{
    public class BankService : IBankService
    {
        private readonly IMediator _bus;

        public BankService(IMediator bus)
        {
            _bus = bus;
        }

        public Task<Guid> CreateNewBankAsync(NewBankViewModel values)
        {
            return _bus.Send(new CreateNewBankCommand(Guid.NewGuid(), values.BankName));
        }

        public Task<List<BankListViewModel>> GetAllBanksAsync()
        {
            return _bus.Send(new GetAllBanksQuery());
        }

        public Task<int> ChargeInterestsAsync(Guid bankId)
        {
            return _bus.Send(new ChargeInterestsCommand(bankId));
        }
    }
}
