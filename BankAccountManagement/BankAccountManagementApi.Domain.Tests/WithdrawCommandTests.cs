using BankAccountManagementApi.Domain.CommandHandlers;
using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Domain.Notifications;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests
{
    public class WithdrawCommandTests
    {
        private readonly Mock<IMediator> _bus;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly AccountCommandHandler _accountCommandHandler;
        private readonly Mock<DomainNotificationHandler> _notificationHandler;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Guid _mockAccountID = Guid.Parse("1B4156A7-222E-4113-BC11-479DF7C01AD5");
        private readonly Guid _mockBankID = Guid.Parse("B10E2E9A-AE83-4B35-971C-6A33C999D55D");

        public WithdrawCommandTests()
        {
            _bus = new Mock<IMediator>();
            _uow = new Mock<IUnitOfWork>();
            _accountRepository = new Mock<IAccountRepository>();
            _notificationHandler = new Mock<DomainNotificationHandler>();

            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            _accountCommandHandler = new AccountCommandHandler(_bus.Object, _uow.Object, _notificationHandler.Object, _accountRepository.Object);

            var newAccount = new Account() { AccountID = _mockAccountID, BankID = _mockBankID, InterestLimit = -111, Interests = 0.2, Money = 0 };
            _accountRepository.Setup(a => a.GetByIdAsync(_mockAccountID)).ReturnsAsync(newAccount);
        }

        [Fact]
        public async Task Should_Withdraw()
        {
            // Arrange
            var newWithdrawCommand = new WithdrawCommand(_mockAccountID, 10);

            // Act
            var amount = await _accountCommandHandler.Handle(newWithdrawCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<WithdrewEvent>(a => a.Amount == amount), CancellationToken.None));

            _uow.Verify(x => x.CommitAsync());

            Assert.Equal(-10, amount);
        }

        [Fact]
        public async Task Should_Not_Withdraw_Invalid_Too_High_Amount()
        {
            // Arrange
            var newWithdrawCommand = new WithdrawCommand(_mockAccountID, 1120);

            // Act
            var amount = await _accountCommandHandler.Handle(newWithdrawCommand, CancellationToken.None);

            // Assert
            _bus.Verify(x => x.Publish(It.Is<WithdrewEvent>(a => a.Amount == amount), CancellationToken.None), Times.Never);

            _uow.Verify(x => x.CommitAsync(), Times.Never);

            _bus.Verify(x => x.Publish(It.Is<DomainNotification>(n => n.Key == ValidationErrorCodes.TooHighAmount), CancellationToken.None));

            Assert.Equal(0, amount);
        }
    }
}
