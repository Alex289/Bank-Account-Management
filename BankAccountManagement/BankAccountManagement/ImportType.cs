using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BankAccountManagement
{

    [XmlRoot("item")]
    public class ImportType
    {
        public Guid Id { get; set; }
        public string BankName { get; set; }
        public List<ImportTypeAccount> Accounts { get; set; }
    }

    public class ImportTypeAccount
    {
        public Guid Id { get; set; }
        public double Money { get; set; }

        public int InterestLimit { get; set; }
        public double Interests { get; set; }
        public Guid BankId { get; set; }
    }
}
