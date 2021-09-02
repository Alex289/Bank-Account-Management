using BankAccountManagementApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.Interfaces
{
    public interface IBankService
    {
        public Task<List<BankListViewModel>> GetAllBanksAsync();
        public Task<Guid> CreateNewBankAsync(NewBankViewModel values);
        public Task<int> ChargeInterestsAsync(Guid bankId);
    }
}
