using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace BankAccountManagement
{
    public class Export
    {
        public void HandleExport(string format, string path, List<Account> userList, List<Bank> bankList)
        {
            switch (format.ToUpper())
            {

                case "JSON":
                    ExportAsJson(bankList, path);
                    break;

                case "CSV":
                    ExportAsCsv(bankList, path, userList);
                    break;

                case "XML":
                    ExportAsXml(bankList, path);
                    break;
                case "DB":
                    ExportAsDb(bankList);
                    break;
                default:
                    Console.WriteLine("Enter a valid format");
                    break;
            }
        }

        public void ExportAsJson(List<Bank> bankList, string path)
        {
            var json = JsonSerializer.Serialize(bankList);

            try
            {
                File.Delete(path);
                File.AppendAllText(path, json);

                Console.WriteLine("Finished exporting data\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void ExportAsCsv(List<Bank> bankList, string path, List<Account> accountList)
        {
            try
            {
                File.Delete(path);

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("Id,Money,InterestLimit,Interests,BankId,BankName");
                    foreach (Account acc in accountList)
                    {
                        sw.WriteLine($"{acc.Id},{acc.Money.ToString().Replace(",", ".")},{acc.InterestLimit},{acc.Interests.ToString().Replace(",", ".")},{acc.BankId}");
                    }
                }

                string ext = new FileInfo(path).Extension;
                if (ext != "")
                {
                    path = Regex.Replace(path, ext, ".bank" + ext);
                }
                else
                {
                    path += "_Bank";
                }

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("BankId,BankName");
                    foreach (Bank bank in bankList)
                    {
                        sw.WriteLine($"{bank.Id},{bank.BankName}");
                    }
                }

                Console.WriteLine("Finished exporting data\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void ExportAsXml(List<Bank> bankList, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ImportType>));

            var exportList = bankList
                .Select(bank => new ImportType
                {
                    Id = bank.Id,
                    BankName = bank.BankName,
                    Accounts = bank.Accounts
                        .Select(account => new ImportTypeAccount
                        {
                            BankId = account.BankId,
                            Id = account.Id,
                            InterestLimit = account.InterestLimit,
                            Interests = account.Interests,
                            Money = account.Money
                        })
                        .ToList()
                })
                .ToList();


            try
            {
                File.Delete(path);
                TextWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, exportList);
                writer.Close();

                Console.WriteLine("Finished exporting data\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void ExportAsDb(List<Bank> list)
        {
            Database db = new Database();
            try
            {
                db.SaveToDatabase(list);
                Console.WriteLine("Finished exporting data\n");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
