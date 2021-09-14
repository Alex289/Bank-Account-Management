using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankContext _context;

        public UnitOfWork(BankContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException);
                return false;
            }
        }

        public async void Dispose()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _context.DisposeAsync();
            }
        }
    }
}
