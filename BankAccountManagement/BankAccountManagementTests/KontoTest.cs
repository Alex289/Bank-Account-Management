using BankAccountManagement;
using System;
using Xunit;

namespace BankAccountManagementTests
{
    public class KontoTest
    {
        [Fact]
        public void Add_Money_To_Account()
        {
            // Arrange
            Bank bank = new Bank("Sparkasse");

            // Act
            Guid accId = bank.NewAccount(-10, .1);
            bank.Deposit(accId, 10);
            double kontoStand = bank.AccountStatus(accId);

            // Assert
            Assert.Equal(10, kontoStand);
        }

        [Fact]
        public void Remove_Money_From_Account()
        {
            // Arrange
            Bank bank = new Bank("Sparkasse");

            // Act
            Guid accId = bank.NewAccount(-10, .1);
            bank.Deposit(accId, 10);
            bank.Withdraw(accId, 5);
            double accountStatus = bank.AccountStatus(accId);

            // Assert
            Assert.Equal(5, accountStatus);
        }

        [Fact]
        public void Calculat_Interest()
        {
            // Arrange
            Bank bank = new Bank("Sparkasse");

            // Act
            Guid accId = bank.NewAccount(-10, 0.2);
            bank.Deposit(accId, 10);
            bank.Withdraw(accId, 20);
            bank.ChargeInterests();
            double accountStatus = bank.AccountStatus(accId);

            // Assert
            Assert.Equal(-12, accountStatus);
        }
    }
}
