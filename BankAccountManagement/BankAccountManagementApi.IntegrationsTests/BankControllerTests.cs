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
    public class BankControllerTests : DefaultFactory
    {
        [Fact, Priority(0)]
        public async Task Should_Create_Bank()
        {
            // Arrange
            var request = new NewBankViewModel() { BankName = "test" };

            // Act
            var response = await TestClient.PostAsJsonAsync("api/bank", request);
            var content = await response.Content.ReadFromJsonAsync<Guid>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Should().NotBeEmpty();
        }

        [Fact, Priority(1)]
        public async Task Should_Get_All_Banks()
        {
            // Arrange
            var firstBankId = Guid.Parse("C4093ECE-6409-4B64-B5A5-FC880EB0D0CB");
            var secondBankId = Guid.Parse("BB135BCF-2213-4118-96BF-2963B6363297");

            // Act
            var response = await TestClient.GetAsync("api/bank");
            var content = await response.Content.ReadFromJsonAsync<List<Bank>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Count.Should().BeGreaterThan(1);

            content.Find(account => account.BankID == firstBankId).Should().NotBeNull();
            content.Find(account => account.BankID == secondBankId).Should().NotBeNull();
        }


        [Fact, Priority(1)]
        public async Task Should_Charge_Interests()
        {
            // Arrange
            var request = Guid.Parse("C4093ECE-6409-4B64-B5A5-FC880EB0D0CB");

            // Act
            var response = await TestClient.GetAsync("api/bank/charge-interests/" + request);
            var content = await response.Content.ReadFromJsonAsync<int>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            content.Should().Be(1);
        }

        [Fact, Priority(1)]
        public async Task Should_Get_Charged_Account()
        {
            // Arrange
            var Request = Guid.Parse("60DACE6C-7493-44A2-B5C3-08C8D5F02585");

            // Act
            var Response = await TestClient.GetAsync("api/account/" + Request);
            var Content = await Response.Content.ReadFromJsonAsync<Account>();

            // Assert
            Response.StatusCode.Should().Be(HttpStatusCode.OK);

            Content.AccountID.Should().Be(Request);
            Content.Money.Should().Be((decimal)-9.6);
        }
    }
}
