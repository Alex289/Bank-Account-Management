using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Repository;
using BankAccountManagementApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountManagementApi.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public void AddAccount(Account account)
        {
            _context.Account.Add(account);
            _context.SaveChanges();
        }

        public IQueryable<Account> GetAll()
        {
            return _context.Account;
        }

        public Account GetById(Guid accountId)
        {
            return _context.Account.Where(item => item.AccountID == accountId)
                .FirstOrDefault();
        }

        public List<Account> GetByBankId(Guid bankId)
        {
            return _context.Account.Where(item => item.BankID == bankId)
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
