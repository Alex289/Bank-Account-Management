﻿using BankAccountManagementApi.Domain.CommandHandlers;
using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Repository;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests
{
    public class DepositCommandTests
    {
        private readonly Mock<IMediator> _bus;
        private readonly AccountCommandHandler _accountCommandHandler;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Guid _mockAccountID = Guid.Parse("1B4156A7-222E-4113-BC11-479DF7C01AD5");
        private readonly Guid _mockBankID = Guid.Parse("B10E2E9A-AE83-4B35-971C-6A33C999D55D");

        public DepositCommandTests()
        {
            _bus = new Mock<IMediator>();
            _accountRepository = new Mock<IAccountRepository>();

            _accountCommandHandler = new AccountCommandHandler(_bus.Object, _accountRepository.Object);

            var newAccount = new Account() { AccountID = _mockAccountID, BankID = _mockBankID, InterestLimit = -111, Interests = 0.2, Money = 0 };
            _accountRepository.Setup(a => a.GetById(_mockAccountID)).Returns(newAccount);
        }

        [Fact]
        public async Task Should_Deposit()
        {
            // Arrange
            var newDepositCommand = new DepositCommand(_mockAccountID, 10);

            // Act
            var amount = await _accountCommandHandler.Handle(newDepositCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<DepositedEvent>(a => a.Amount == amount), CancellationToken.None));

            _accountRepository.Verify(x => x.SaveChanges());

            Assert.Equal(10, amount);
        }

        [Fact]
        public async Task Should_Not_Deposit_Invalid_Empty_Guid()
        {
            // Arrange
            var newDepositCommand = new DepositCommand(Guid.Empty, 10);

            // Act
            var amount = await _accountCommandHandler.Handle(newDepositCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<DepositedEvent>(a => a.Amount == amount), CancellationToken.None), Times.Never);

            _accountRepository.Verify(x => x.SaveChanges(), Times.Never);

            Assert.Equal(0, amount);
        }
    }
}
