using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BankAccountManagement
{
    class Menues
    {
        List<Bank> accounts = new List<Bank>();

        public User MainMenue(User user)
        {
            while (user == null)
            {
                Console.WriteLine("---BankAccountManagement---");
                Console.WriteLine("---------Main Menu---------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - List all banks and accounts");
                Console.WriteLine("\t2 - Add new bank");
                Console.WriteLine("\t3 - Add new account");
                Console.WriteLine("\t4 - Log in");
                Console.WriteLine("\t5 - Export data");
                Console.WriteLine("\t6 - Import data");
                Console.WriteLine("\t7 - End app");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();

                        foreach (Bank curBank in accounts)
                        {
                            Console.WriteLine("Bank:");
                            Console.WriteLine(curBank.BankName + " " + curBank.Id + "\n");
                            Console.WriteLine("Accounts:");
                            foreach (Account acc in curBank.Accounts)
                            {
                                Console.WriteLine(acc.Id);
                            }
                            Console.WriteLine("\n\n");
                        }

                        Console.WriteLine();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter the name of the new bank: ");

                        Bank newBank = new Bank(Console.ReadLine());

                        accounts.Add(newBank);

                        Console.WriteLine("ID for new bank is: " + newBank.Id);
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter the bank you want to access:");
                        Guid bankId = Guid.Parse(Console.ReadLine());

                        Bank bank = accounts.Find(bank => bank.Id == bankId);
                        if (bank != null)
                        {
                            Console.WriteLine("Enter the account limit (e.g. -100): ");
                            int accountLimit = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter the interest rate (e.g. 0,1): ");
                            double interests = Convert.ToDouble(Console.ReadLine());

                            Guid userID = bank.NewAccount(accountLimit, interests);
                            Console.WriteLine("ID for new account is: " + userID + "\n");

                            user = new User(bankId, userID);
                        }
                        else
                        {
                            Console.WriteLine("Bank does not exist! \n");
                        }
                        break;

                    case "4":
                        Console.Clear();

                        Console.WriteLine("Enter the bankID you want to access:");
                        Guid LogInBank = Guid.Parse(Console.ReadLine());

                        Console.WriteLine("Enter your account ID:");

                        Guid logInID = Guid.Parse(Console.ReadLine());

                        Console.Clear();
                        if (accounts.Find(item => item.Id == LogInBank && item.Accounts.Count() > 0) != null)
                        {
                            user = new User(LogInBank, logInID);
                        }
                        else
                        {
                            Console.WriteLine("Account does not exist in this bank");
                        }

                        break;

                    case "5":
                        Console.Clear();

                        Console.WriteLine("Enter the format you want to export (Json, Csv, Xml, db(database)):");
                        string format = Console.ReadLine();

                        Console.WriteLine(@"Enter the path and filename to save the file to (e.g. Exports\List.json)(Hit enter on db):");

                        var defaultPath = Assembly.GetExecutingAssembly().Location.Split(@"BankAccountManagement\bin")[0] + @"Exports\List." + format.ToLower();
                        Console.WriteLine("Recommended path: " + defaultPath);

                        string path = Console.ReadLine();

                        List<Account> userList = new List<Account>();

                        foreach (Bank account in accounts)
                        {
                            userList.AddRange(account.Accounts);
                        }

                        Console.Clear();

                        Export exporthandler = new Export();
                        exporthandler.HandleExport(format, path, userList, accounts);

                        break;

                    case "6":
                        Console.Clear();

                        Console.WriteLine("Enter the format you want to import (Json, Csv, Xml, db(database)):");
                        format = Console.ReadLine();

                        Console.WriteLine(@"Enter the path and filename of your file (e.g. Exports\List.json)(Hit enter on db):");

                        defaultPath = Assembly.GetExecutingAssembly().Location.Split(@"BankAccountManagement\bin")[0] + @"Exports\List." + format.ToLower();
                        Console.WriteLine("Recommended path: " + defaultPath);

                        path = Console.ReadLine();

                        Console.Clear();

                        Import importHandler = new Import();
                        accounts = importHandler.HandleImport(format, path);

                        Console.WriteLine("Imported data successfully\n");
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
                Bank bank = accounts.Find(bank => bank.Id == user.BankID);

                Console.WriteLine("---BankAccountManagement---");
                Console.WriteLine("---------User Menu---------");
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\t1 - Log out");
                Console.WriteLine("\t2 - Account status");
                Console.WriteLine("\t3 - Deposit");
                Console.WriteLine("\t4 - Withdraw");
                Console.WriteLine("\t5 - Charge interests");
                Console.WriteLine("\t6 - End App");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();

                        user = null;
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Your account status is: " + bank.AccountStatus(user.ID) + "\n");
                        break;

                    case "3":
                        Console.Clear();

                        Console.WriteLine("Enter the amount you want to deposit: ");
                        double DepositAmount = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Your account status is now: " + bank.Deposit(user.ID, DepositAmount) + "\n");
                        break;

                    case "4":
                        Console.Clear();

                        Console.WriteLine("Enter the amount you want to withdraw: ");
                        double WithdrawAmount = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Your account status is now: " + bank.Withdraw(user.ID, WithdrawAmount) + "\n");
                        break;

                    case "5":
                        Console.Clear();

                        bank.ChargeInterests();

                        Console.WriteLine("Interests on all accounts charged \n");
                        break;

                    case "6":
                        Console.Clear();
                        System.Environment.Exit(1);
                        break;
                }
            }

            return user;
        }
    }
}
