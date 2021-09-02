using System;

namespace BankAccountManagementApi.Domain.Entities
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public decimal Money { get; set; }
        public int InterestLimit { get; set; }
        public double Interests { get; set; }
        public Guid BankID { get; set; }
    }
}
