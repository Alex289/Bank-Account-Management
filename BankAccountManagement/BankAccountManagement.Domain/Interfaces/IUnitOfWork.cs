using BankAccountManagementApi.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public Task<bool> CommitAsync();
    }
}
