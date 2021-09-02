using System;

namespace BankAccountManagementApi.Application.ViewModels
{
    public class AccountListViewModel
    {
        public Guid AccountID { get; set; }
        public decimal Money { get; set; }
        public int InterestLimit { get; set; }
        public double Interests { get; set; }
        public Guid BankID { get; set; }
    }
}
