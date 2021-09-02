using BankAccountManagementApi.Domain.Entities;
using System;
using System.Linq;

namespace BankAccountManagementApi.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(BankContext context)
        {
            context.Database.EnsureCreated();

            if (context.Bank.Any())
            {
                return;
            }

            var firstBank = new Bank() { BankName = "Sparkasse", BankID = Guid.Parse("C4093ECE-6409-4B64-B5A5-FC880EB0D0CB") };
            var secondBank = new Bank() { BankName = "Deutsche Bank", BankID = Guid.Parse("BB135BCF-2213-4118-96BF-2963B6363297") };
            var firstAccount = new Account() { AccountID = Guid.Parse("FE2AB83E-8126-4222-8805-2861959AB29C"), BankID = firstBank.BankID, InterestLimit = -111, Interests = 0.2, Money = 10 };
            var secondAccount = new Account() { AccountID = Guid.Parse("60DACE6C-7493-44A2-B5C3-08C8D5F02585"), BankID = firstBank.BankID, InterestLimit = -111, Interests = 0.2, Money = -8 };

            context.Bank.Add(firstBank);
            context.Bank.Add(secondBank);
            context.SaveChanges();

            context.Account.Add(firstAccount);
            context.Account.Add(secondAccount);
            context.SaveChanges();
        }
    }
}
