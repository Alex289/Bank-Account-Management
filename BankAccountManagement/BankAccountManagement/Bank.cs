using System;
using System.Collections.Generic;

namespace BankAccountManagement
{
    public class Bank
    {
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
