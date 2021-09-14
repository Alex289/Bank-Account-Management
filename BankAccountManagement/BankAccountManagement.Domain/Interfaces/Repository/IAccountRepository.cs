using BankAccountManagementApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Interfaces.Repository
{
    public interface IAccountRepository
    {
        public Task AddAccountAsync(Account account);
        public IQueryable<Account> GetAll();
        public Task<Account> GetByIdAsync(Guid accountId);
        public Task<List<Account>> GetByBankIdAsync(Guid bankId);
    }
}
