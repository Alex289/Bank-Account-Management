using System;

namespace BankAccountManagement.Requests
{
    public class NewAccount
    {
        public decimal Money { get; set; } = 0;
        public int InterestLimit { get; set; }
        public double Interests { get; set; }
        public Guid BankID { get; set; }
    }
}
