using System;

namespace BankAccountManagement
{
    class User
    {
        public Guid BankID { get; set; }
        public Guid ID { get; set; }

        public User(Guid name, Guid id)
        {
            BankID = name;
            ID = id;
        }
    }
}
