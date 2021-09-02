using BankAccountManagementApi.Domain.Entities;
using System.Linq;

namespace BankAccountManagementApi.Domain.Repository
{
    public interface IBankRepository
    {
        public void AddBank(Bank bank);
        public IQueryable<Bank> GetAll();
    }
}
