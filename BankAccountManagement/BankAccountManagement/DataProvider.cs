using BankAccountManagement.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement
{
    public static class DataProvider
    {
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44305/api/");
            return client;
        }

        // Bank endpoints

        public static async Task<List<Bank>> GetAllBanksAsync()
        {
            var client = CreateClient();

            var response = await client.GetAsync("bank");
            return await response.Content.ReadFromJsonAsync<List<Bank>>();
        }

        public static async Task<Guid> PostNewBankAsync(NewBank bank)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("bank", bank);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public static async Task<int> ChargeInterestsAsync(Guid bankId)
        {
            var client = CreateClient();

            var response = await client.GetAsync("bank/charge-interests/" + bankId);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        // Account endpoints

        public static async Task<List<Account>> GetAllAccountsAsync()
        {
            var client = CreateClient();

            var response = await client.GetAsync("account");
            return await response.Content.ReadFromJsonAsync<List<Account>>();
        }

        public static async Task<Guid> PostNewAccountAsync(NewAccount account)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("account", account);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public static async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            var client = CreateClient();

            var response = await client.GetAsync("account/" + accountId);
            return await response.Content.ReadFromJsonAsync<Account>();
        }

        public static async Task<List<Account>> GetAccountByBankIdAsync(Guid bankId)
        {
            var client = CreateClient();

            var response = await client.GetAsync("account/bank/" + bankId);
            return await response.Content.ReadFromJsonAsync<List<Account>>();
        }

        public static async Task<decimal> DepositAsync(Guid accountId, Amount amount)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("account/deposit/" + accountId, amount);
            return await response.Content.ReadFromJsonAsync<decimal>();
        }

        public static async Task<decimal> WithdrawAsync(Guid accountId, Amount amount)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("account/withdraw/" + accountId, amount);
            return await response.Content.ReadFromJsonAsync<decimal>();
        }
    }
}
