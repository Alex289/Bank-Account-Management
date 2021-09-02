using System;
using System.Collections.Generic;

namespace BankAccountManagement
{
    public class Bank
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BankName { get; set; }

        private List<Account> accounts = new List<Account>();
        public List<Account> Accounts => accounts;

        public Bank(string name)
        {
            BankName = name;
        }

        public Guid NewAccount(int interestLimit, double interests)
        {
            var account = new Account(interestLimit, interests, Id);
            accounts.Add(account);
            return account.Id;
        }

        public double AccountStatus(Guid accountId)
        {
            return accounts.Find(acc => acc.Id == accountId).Money;
        }

        public double Deposit(Guid accountId, double amount)
        {
            return accounts.Find(acc => acc.Id == accountId).Deposit(amount);
        }

        public double Withdraw(Guid accountId, double amount)
        {
            return accounts.Find(acc => acc.Id == accountId).Withdraw(amount);
        }

        public void ChargeInterests()
        {
            foreach (Account account in accounts)
            {
                account.ChargeInterests();
            }
        }

        public void ImportAccount(Account acc)
        {
            accounts.Add(acc);
        }
    }
}
