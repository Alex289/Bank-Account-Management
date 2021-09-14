using BankAccountManagementApi.Domain.CommandHandlers;
using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Domain.Notifications;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests
{
    public class ChargeInterestsCommandTests
    {
        private readonly Mock<IMediator> _bus;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly BankCommandHandler _bankCommandHandler;
        private readonly Mock<DomainNotificationHandler> _notificationHandler;
        private readonly Mock<IBankRepository> _bankRepository;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Guid _mockAccountID = Guid.Parse("1B4156A7-222E-4113-BC11-479DF7C01AD5");
        private readonly Guid _mockBankID = Guid.Parse("B10E2E9A-AE83-4B35-971C-6A33C999D55D");

        public ChargeInterestsCommandTests()
        {
            _bus = new Mock<IMediator>();
            _uow = new Mock<IUnitOfWork>();
            _bankRepository = new Mock<IBankRepository>();
            _accountRepository = new Mock<IAccountRepository>();
            _notificationHandler = new Mock<DomainNotificationHandler>();

            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            _bankCommandHandler = new BankCommandHandler(_bus.Object, _uow.Object, _notificationHandler.Object, _bankRepository.Object, _accountRepository.Object);

            var accountList = new List<Account>();
            var newAccount = new Account() { AccountID = _mockAccountID, BankID = _mockBankID, InterestLimit = -111, Interests = 0.2, Money = -8 };
            accountList.Add(newAccount);

            _accountRepository.Setup(a => a.GetByBankIdAsync(_mockBankID)).ReturnsAsync(accountList);
        }

        [Fact]
        public async Task Should_Charge_Interests()
        {
            // Arrange
            var chargeInterestsCommand = new ChargeInterestsCommand(_mockBankID);

            // Act
            var chargedAccountsNumber = await _bankCommandHandler.Handle(chargeInterestsCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<ChargedInterestsEvent>(b => b.ChargedAccountsNumber == chargedAccountsNumber), CancellationToken.None));

            _uow.Verify(x => x.CommitAsync());

            Assert.Equal(1, chargedAccountsNumber);
        }

        [Fact]
        public async Task Should_Not_Charge_Interests_Invalid_Empty_Guid()
        {
            // Arrange
            var chargeInterestsCommand = new ChargeInterestsCommand(Guid.Empty);

            // Act
            var chargedAccountsNumber = await _bankCommandHandler.Handle(chargeInterestsCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<ChargedInterestsEvent>(b => b.ChargedAccountsNumber == chargedAccountsNumber), CancellationToken.None), Times.Never);

            _uow.Verify(x => x.CommitAsync(), Times.Never);

            _bus.Verify(x => x.Publish(It.Is<DomainNotification>(n => n.Key == ValidationErrorCodes.EmptyBankId), CancellationToken.None));

            Assert.Equal(0, chargedAccountsNumber);
        }
    }
}
