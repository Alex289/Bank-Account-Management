using BankAccountManagementApi.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Interfaces.Repository
{
    public interface IBankRepository
    {
        public Task AddBankAsync(Bank bank);
        public IQueryable<Bank> GetAll();
    }
}
