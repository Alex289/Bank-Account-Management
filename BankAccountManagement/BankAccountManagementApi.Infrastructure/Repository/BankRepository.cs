using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Repository;
using BankAccountManagementApi.Infrastructure.Data;
using System.Linq;

namespace BankAccountManagementApi.Infrastructure.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankContext _context;

        public BankRepository(BankContext context)
        {
            _context = context;
        }

        public void AddBank(Bank bank)
        {
            _context.Bank.Add(bank);
            _context.SaveChanges();
        }

        public IQueryable<Bank> GetAll()
        {
            return _context.Bank;
        }
    }
}
