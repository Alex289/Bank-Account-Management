using BankAccountManagement.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace BankAccountManagement
{
    class Menues
    {
        List<Bank> bankList;
        List<Account> accountList;

        public User MainMenue(User user)
        {
            bankList = DataProvider.GetAllBanksAsync().Result;
            accountList = DataProvider.GetAllAccountsAsync().Result;

            while (user == null)
            {
                Console.WriteLine("---BankAccountManagement---");
                Console.WriteLine("---------Main Menu---------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - List all banks and accounts");
                Console.WriteLine("\t2 - List all accounts from a bank");
                Console.WriteLine("\t3 - Add new bank");
                Console.WriteLine("\t4 - Add new account");
                Console.WriteLine("\t5 - Charge interests");
                Console.WriteLine("\t6 - Log in");
                Console.WriteLine("\t7 - End app");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();

                        foreach (Bank currentBank in bankList)
                        {
                            Console.WriteLine("Bank:");
                            Console.WriteLine(currentBank.BankName + " " + currentBank.BankID + "\n");
                            Console.WriteLine("Accounts:");

                            foreach (Account account in accountList)
                            {
                                if (account.BankID == currentBank.BankID)
                                {
                                    Console.WriteLine("ID: " + account.AccountID);
                                    Console.WriteLine("Interest limit: " + account.InterestLimit);
                                    Console.WriteLine("Interests: " + account.Interests);
                                    Console.WriteLine("Money: " + account.Money + "\n");
                                }
                            }

                            Console.WriteLine("\n\n");
                        }

                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Clear();

                        Console.WriteLine("Enter the BankID: ");
                        Guid bankId = Guid.Parse(Console.ReadLine());

                        List<Account> accountyByBank = DataProvider.GetAccountByBankIdAsync(bankId).Result;

                        Console.WriteLine();

                        foreach (Account accountsByBank in accountyByBank)
                        {
                            Console.WriteLine("Account:");
                            Console.WriteLine("ID: " + accountsByBank.AccountID);
                            Console.WriteLine("Interest limit: " + accountsByBank.InterestLimit);
                            Console.WriteLine("Interests: " + accountsByBank.Interests);
                            Console.WriteLine("Money: " + accountsByBank.Money + "\n");
                        }

                        Console.WriteLine();
                        break;

                    case "3":
                        Console.Clear();

                        Console.WriteLine("Enter the name of the new bank: ");
                        string newBankName = Console.ReadLine();

                        var newBank = new NewBank() { BankName = newBankName };

                        Guid newBankId = DataProvider.PostNewBankAsync(newBank).Result;
                        bankList = DataProvider.GetAllBanksAsync().Result;

                        Console.WriteLine("ID for new bank is: " + newBankId);
                        break;

                    case "4":
                        Console.Clear();

                        Console.WriteLine("Enter the bank you want to access:");
                        Guid bankIdForAccount = Guid.Parse(Console.ReadLine());

                        Bank bank = bankList.Find(bank => bank.BankID == bankIdForAccount);

                        if (bank != null)
                        {
                            Console.WriteLine("Enter the account limit (e.g. -100): ");
                            int accountLimit = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter the interest rate (e.g. 0,1): ");
                            double accountInterests = Convert.ToDouble(Console.ReadLine());

                            var newAccount = new NewAccount() { InterestLimit = accountLimit, Interests = accountInterests, BankID = bankIdForAccount };

                            Guid newAccountId = DataProvider.PostNewAccountAsync(newAccount).Result;
                            accountList = DataProvider.GetAllAccountsAsync().Result;

                            Console.WriteLine("ID for new account is: " + newAccountId + "\n");

                            user = new User(bankIdForAccount, newAccountId);
                        }
                        else
                        {
                            Console.WriteLine("Bank does not exist! \n");
                        }
                        break;

                    case "5":
                        Console.Clear();
                        
                        Console.WriteLine("Enter the bankID to charge their accounts:");
                        Guid chargedBankId = Guid.Parse(Console.ReadLine());

                        Bank chargedBank = bankList.Find(bank => bank.BankID == chargedBankId);

                        if (chargedBank != null)
                        {
                            int chargedAccountsNumber = DataProvider.ChargeInterestsAsync(chargedBankId).Result;

                            Console.WriteLine("Amount of charged accounts: " + chargedAccountsNumber + "\n");
                        }
                        else
                        {
                            Console.WriteLine("Bank does not exist! \n");
                        }
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Enter your account ID:");

                        Guid logInID = Guid.Parse(Console.ReadLine());

                        Console.Clear();

                        Account currentAccount = accountList.Find(item => item.AccountID == logInID);
                        if (currentAccount != null)
                        {
                            user = new User(currentAccount.BankID, currentAccount.AccountID);
                        }
                        else
                        {
                            Console.WriteLine("Account does not exist");
                        }

                        break;

                    case "7":
                        Console.Clear();
                        System.Environment.Exit(1);
                        break;
                }
            }

            return user;

        }

        public User UserMenu(User user)
        {
            while (user != null)
            {
                bankList = DataProvider.GetAllBanksAsync().Result;
                accountList = DataProvider.GetAllAccountsAsync().Result;

                Account account = accountList.Find(item => item.AccountID == user.ID);

                Console.WriteLine("---BankAccountManagement---");
                Console.WriteLine("---------User Menu---------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - Log out");
                Console.WriteLine("\t2 - Account status");
                Console.WriteLine("\t3 - Deposit");
                Console.WriteLine("\t4 - Withdraw");
                Console.WriteLine("\t5 - End App");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();

                        user = null;
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Your account status is: " + account.Money + "\n");
                        break;

                    case "3":
                        Console.Clear();

                        Console.WriteLine("Enter the amount you want to deposit: ");
                        decimal DepositAmount = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("Your account status is now: " + DataProvider.DepositAsync(user.ID, new Amount() { amount = DepositAmount }).Result + "\n");
                        break;

                    case "4":
                        Console.Clear();

                        Console.WriteLine("Enter the amount you want to withdraw: ");
                        decimal WithdrawAmount = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("Your account status is now: " + DataProvider.WithdrawAsync(user.ID, new Amount() { amount = WithdrawAmount }).Result + "\n");
                        break;

                    case "5":
                        Console.Clear();
                        System.Environment.Exit(1);
                        break;
                }
            }

            return user;
        }
    }
}
