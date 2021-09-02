using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace BankAccountManagement
{
    public class Import
    {
        public List<Bank> HandleImport(string format, string path)
        {
            List<Bank> bankList = new List<Bank>();
            switch (format.ToUpper())
            {
                case "JSON":
                    bankList = ImportJson(path);
                    break;

                case "CSV":
                    bankList = ImportCsv(path);
                    break;

                case "XML":
                    bankList = ImportXml(path);
                    break;
                case "DB":
                    bankList = ImportDb();
                    break;
                default:
                    Console.WriteLine("Enter a valid format");
                    break;

            }
            return bankList;
        }

        public List<Bank> ImportJson(string path)
        {
            string json = "";

            try
            {
                json = System.IO.File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }

            List<ImportType> deserializedJson = JsonSerializer.Deserialize<List<ImportType>>(json);

            return TransformListType(deserializedJson);
        }

        private static List<Bank> TransformListType(List<ImportType> deserializedJson)
        {
            List<Bank> bankList = new List<Bank>();

            foreach (var bankData in deserializedJson)
            {
                Bank bank = new Bank(bankData.BankName);
                bank.Id = bankData.Id;

                foreach (var accountData in bankData.Accounts)
                {
                    var account = new Account(accountData.InterestLimit, accountData.Interests, accountData.BankId, accountData.Money);
                    account.Id = accountData.Id;
                    bank.ImportAccount(account);
                }

                bankList.Add(bank);
            }
            return bankList;
        }

        public List<Bank> ImportCsv(string path)
        {
            List<Bank> accList = new List<Bank>();
            string ext = new FileInfo(path).Extension;

            if (ext != "")
            {
                path = Regex.Replace(path, ext, ".bank" + ext);
            }
            else
            {
                path += "_Bank";
            }

            try
            {
                using (var reader = new StreamReader(path))
                {
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        Bank bank = new Bank(values[1]);
                        bank.Id = Guid.Parse(values[0]);

                        accList.Add(bank);
                    }
                }

                if (ext != "")
                {
                    path = Regex.Replace(path, ".bank" + ext, ext);
                }
                else
                {
                    path = Regex.Replace(path, "_Bank", "");
                }

                using (var reader = new StreamReader(path))
                {
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        foreach (Bank bank in accList)
                        {
                            if (bank.Id == Guid.Parse(values[4]))
                            {
                                var account = new Account(Convert.ToInt32(values[2]), Convert.ToDouble(values[3].Replace(".", ",")), Guid.Parse(values[4]), Convert.ToDouble(values[1].Replace(".", ",")));
                                account.Id = Guid.Parse(values[0]);

                                bank.ImportAccount(account);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            return accList;
        }

        public List<Bank> ImportXml(string path)
        {
            var serializer = new XmlSerializer(typeof(List<ImportType>));
            List<ImportType> derserialized = new List<ImportType>();

            try
            {
                using (var reader = new StreamReader(path))
                {
                    derserialized = (List<ImportType>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            return TransformListType(derserialized);
        }

        public List<Bank> ImportDb()
        {
            Database db = new Database();
            try
            {
                return db.GetFromDatabase();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
