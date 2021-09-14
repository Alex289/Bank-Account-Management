using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Account.AddAsync(account);
        }

        public IQueryable<Account> GetAll()
        {
            return _context.Account;
        }

        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            return await _context.Account.Where(item => item.AccountID == accountId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetByBankIdAsync(Guid bankId)
        {
            return await _context.Account.Where(item => item.BankID == bankId)
                .ToListAsync();
        }
    }
}
