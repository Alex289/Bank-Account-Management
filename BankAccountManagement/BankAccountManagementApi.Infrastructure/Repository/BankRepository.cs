using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Infrastructure.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankContext _context;

        public BankRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddBankAsync(Bank bank)
        {
            await _context.Bank.AddAsync(bank);
        }

        public IQueryable<Bank> GetAll()
        {
            return _context.Bank;
        }
    }
}
