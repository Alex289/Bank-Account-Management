using BankAccountManagementApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountManagementApi.Domain.Repository
{
    public interface IAccountRepository
    {
        public void AddAccount(Account account);
        public IQueryable<Account> GetAll();
        public Account GetById(Guid accountId);
        public List<Account> GetByBankId(Guid bankId);
        public void SaveChanges();
    }
}
