using System;

namespace BankAccountManagement
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Money { get; private set; }

        public int InterestLimit { get; private set; }
        public double Interests { get; private set; }
        public Guid BankId { get; set; }

        public Account(int interestLimit, double interests, Guid bankId, double money = 0)
        {
            InterestLimit = interestLimit;
            Interests = interests;
            BankId = bankId;
            Money = money;
        }

        public double Deposit(double amount)
        {
            Money += amount;
            return Money;
        }

        public double Withdraw(double amount)
        {
            if (Money - amount < InterestLimit)
            {
                Console.WriteLine("Failed: credit limit would be exceeded");
                return 0;
            }
            else
            {
                Money -= amount;
                return Money;
            }
        }

        public void ChargeInterests()
        {
            if (Money < 0)
            {
                Money += Money * Interests;
            }
        }
    }
}
