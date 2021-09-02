using BankAccountManagementApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountManagementApi.Infrastructure.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().ToTable("Bank");
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
