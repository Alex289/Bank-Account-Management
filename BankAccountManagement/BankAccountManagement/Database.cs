using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement
{
    class Database
    {
        // https://www.connectionstrings.com/sql-server-2019/
        private const string connectionString = @"Server=localhost\sqlexpress;Database=BankAccountManagement;Trusted_Connection=True;";

        public List<Bank> GetFromDatabase()
        {
            List<Bank> banks = new List<Bank>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Bank", connection);
                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bank bank = new Bank(reader.GetString(1));
                        bank.Id = reader.GetGuid(0);
                        banks.Add(bank);
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Account", connection);
                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bank currentBank = banks.Find(bank => bank.Id == reader.GetGuid(4));
                        var account = new Account(
                            reader.GetInt32(2),
                            Convert.ToDouble(reader.GetValue(3)),
                            reader.GetGuid(4),
                            Convert.ToDouble(reader.GetValue(1)));
                        account.Id = reader.GetGuid(0);


                        currentBank.ImportAccount(account);
                    }
                }
            }

            return banks;
        }

        public void ExecuteQuery(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveToDatabase(List<Bank> list)
        {
            foreach (Bank bank in list)
            {
                Console.WriteLine("Try exporting bank data for: " + bank.BankName);

                ExecuteQuery($"INSERT INTO Bank (BankID, BankName) VALUES(" +
                    $"'{bank.Id}', " +
                    $"'{bank.BankName}')");

                foreach (Account Account in bank.Accounts)
                {
                    ExecuteQuery($"INSERT INTO Account (AccountID, Money, InterestLimit, Interests, BankID) VALUES(" +
                        $"'{Account.Id}', " +
                        $"'{Account.Money.ToString().Replace(",", ".")}', " +
                        $"'{Account.InterestLimit}', " +
                        $"'{Account.Interests.ToString().Replace(",", ".")}', " +
                        $"'{bank.Id}')");
                }
            }
        }
    }
}
