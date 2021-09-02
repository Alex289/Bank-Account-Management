using System;
using System.Collections.Generic;

namespace BankAccountManagementApi.Domain.Entities
{
    public class Bank
    {
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
