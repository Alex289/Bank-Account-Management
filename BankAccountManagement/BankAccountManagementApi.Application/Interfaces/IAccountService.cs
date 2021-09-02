using BankAccountManagementApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<Guid> NewAccountAsync(NewAccountViewModel values);
        public Task<List<AccountListViewModel>> GetAllAccountsAsync();
        public Task<List<AccountListViewModel>> GetByBankIdAsync(Guid bankId);
        public Task<AccountListViewModel> GetByAccountIdAsync(Guid accountId);
        public Task<decimal> DepositAsync(Guid accountId, DepositWithdrawViewModel amount);
        public Task<decimal> WithdrawAsync(Guid accountId, DepositWithdrawViewModel amount);
    }
}
