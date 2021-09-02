using BankAccountManagement;
using System;
using Xunit;

namespace BankAccountManagementTests
{
    public class BankTest
    {
        [Fact]
        public void Create_New_Account()
        {
            // Arrange
            Bank bank = new Bank("Sparkasse");

            // Act
            Guid accId = bank.NewAccount(-10, .1);
            double accountStatus = bank.AccountStatus(accId);

            // Assert
            Assert.Equal(0, accountStatus);
        }
    }
}
