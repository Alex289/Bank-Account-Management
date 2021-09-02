using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace BankAccountManagementApi.IntegrationsTests
{
    [Collection("BankAccountManagementApi")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class AccountControllerTests : DefaultFactory
    {
        static Guid newAccountId;

        [Fact, Priority(3)]
        public async Task Should_Get_All_Accounts()
        {
            // Arrange
            var firstAccountId = Guid.Parse("60DACE6C-7493-44A2-B5C3-08C8D5F02585");
            var secondAccountId = Guid.Parse("FE2AB83E-8126-4222-8805-2861959AB29C");

            // Act
            var response = await TestClient.GetAsync("api/account");
            var content = await response.Content.ReadFromJsonAsync<List<Account>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Count.Should().BeGreaterThan(1);

            content.Find(account => account.AccountID == firstAccountId).Should().NotBeNull();
            content.Find(account => account.AccountID == secondAccountId).Should().NotBeNull();
        }

        [Fact, Priority(2)]
        public async Task Should_Create_Account()
        {
            // Arrange
            var request = new NewAccountViewModel() { BankID = Guid.Parse("BB135BCF-2213-4118-96BF-2963B6363297"), InterestLimit = -111, Interests = 0.5, Money = 187 };

            // Act
            var response = await TestClient.PostAsJsonAsync("api/account", request);
            var content = await response.Content.ReadFromJsonAsync<Guid>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Should().NotBeEmpty();

            newAccountId = content;
        }

        [Fact, Priority(3)]
        public async Task Should_Get_New_Account()
        {
            // Arrange
            var bankId = Guid.Parse("BB135BCF-2213-4118-96BF-2963B6363297");

            // Act
            var response = await TestClient.GetAsync("api/account/" + newAccountId);
            var content = await response.Content.ReadFromJsonAsync<Account>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.AccountID.Should().Be(newAccountId);
            content.BankID.Should().Be(bankId);
        }

        [Fact, Priority(3)]
        public async Task Should_Get_By_Id()
        {
            // Arrange
            var request = Guid.Parse("FE2AB83E-8126-4222-8805-2861959AB29C");

            // Act
            var response = await TestClient.GetAsync("api/account/" + request);
            var content = await response.Content.ReadFromJsonAsync<Account>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.AccountID.Should().Be(request);
        }

        [Fact, Priority(3)]
        public async Task Should_Get_By_Bank_Id()
        {
            // Arrange
            var request = Guid.Parse("C4093ECE-6409-4B64-B5A5-FC880EB0D0CB");

            // Act
            var response = await TestClient.GetAsync("api/account/bank/" + request);
            var content = await response.Content.ReadFromJsonAsync<List<Account>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.TrueForAll(account => account.BankID == request).Should().Be(true);
        }

        [Fact, Priority(3)]
        public async Task Should_Deposit()
        {
            // Arrange
            Guid accountId = Guid.Parse("FE2AB83E-8126-4222-8805-2861959AB29C");
            var request = new DepositWithdrawViewModel() { Amount = 10 };

            // Act
            var response = await TestClient.PostAsJsonAsync("api/account/deposit/" + accountId, request);
            var content = await response.Content.ReadFromJsonAsync<decimal>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Should().Be(20);
        }

        [Fact, Priority(3)]
        public async Task Should_Withdraw()
        {
            // Arrange
            Guid accountId = Guid.Parse("FE2AB83E-8126-4222-8805-2861959AB29C");
            var request = new DepositWithdrawViewModel() { Amount = 10 };

            // Act
            var response = await TestClient.PostAsJsonAsync("api/account/withdraw/" + accountId, request);
            var content = await response.Content.ReadFromJsonAsync<decimal>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Should().Be(10);
        }
    }
}
