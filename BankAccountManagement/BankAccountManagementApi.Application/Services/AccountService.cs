using BankAccountManagementApi.Application.Interfaces;
using BankAccountManagementApi.Application.Queries.Account;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Commands.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMediator _bus;

        public AccountService(IMediator bus)
        {
            _bus = bus;
        }

        public Task<Guid> NewAccountAsync(NewAccountViewModel values)
        {
            return _bus.Send(new CreateNewAccountCommand(values.Money, values.InterestLimit, values.Interests, values.BankID));
        }

        public Task<List<AccountListViewModel>> GetAllAccountsAsync()
        {
            return _bus.Send(new GetAllAccountsQuery());
        }

        public Task<List<AccountListViewModel>> GetByBankIdAsync(Guid bankId)
        {
            return _bus.Send(new GetAccountByBankIdQueryAsync(bankId));
        }

        public Task<AccountListViewModel> GetByAccountIdAsync(Guid accountId)
        {
            return _bus.Send(new GetAccountsByIdQuery(accountId));
        }

        public Task<decimal> DepositAsync(Guid accountId, DepositWithdrawViewModel values)
        {
            return _bus.Send(new DepositCommand(accountId, values.Amount));
        }

        public Task<decimal> WithdrawAsync(Guid accountId, DepositWithdrawViewModel values)
        {
            return _bus.Send(new WithdrawCommand(accountId, values.Amount));
        }
    }
}
