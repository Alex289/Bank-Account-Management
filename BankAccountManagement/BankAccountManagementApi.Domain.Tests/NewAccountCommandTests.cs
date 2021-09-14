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
    public class NewAccountCommandTests
    {
        private readonly Mock<IMediator> _bus;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly AccountCommandHandler _accountCommandHandler;
        private readonly Mock<DomainNotificationHandler> _notificationHandler;
        private readonly Mock<IAccountRepository> _accountRepository;

        public NewAccountCommandTests()
        {
            _bus = new Mock<IMediator>();
            _uow = new Mock<IUnitOfWork>();
            _accountRepository = new Mock<IAccountRepository>();
            _notificationHandler = new Mock<DomainNotificationHandler>();

            _uow.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            _accountCommandHandler = new AccountCommandHandler(_bus.Object, _uow.Object, _notificationHandler.Object, _accountRepository.Object);
        }

        [Fact]
        public async Task Should_Create_New_Account()
        {
            // Arrange
            var newAccountCommand = new CreateNewAccountCommand(100, -111, 0.5, Guid.NewGuid());

            // Act
            var newaccountId = await _accountCommandHandler.Handle(newAccountCommand, CancellationToken.None);

            // Assert
            _accountRepository.Verify(x => x.AddAccountAsync(It.Is<Account>(a => a.BankID == newAccountCommand.BankID && a.AccountID == newaccountId)));

            _uow.Verify(x => x.CommitAsync());

            _bus.Verify(x => x.Publish(It.Is<CreatedAccountEvent>(a => a.AccountID == newaccountId), CancellationToken.None));

            Assert.NotEqual(newaccountId, Guid.Empty);
        }

        [Fact]
        public async Task Should_Not_Create_New_Account_Invalid_Too_Low_Interests()
        {
            // Arrange
            var newAccountCommand = new CreateNewAccountCommand(100, -111, -2, Guid.NewGuid());

            // Act
            var newaccountId = await _accountCommandHandler.Handle(newAccountCommand, CancellationToken.None);

            // Assert
            _accountRepository.Verify(x => x.AddAccountAsync(It.Is<Account>(a => a.BankID == newAccountCommand.BankID && a.AccountID == newaccountId)), Times.Never);

            _uow.Verify(x => x.CommitAsync(), Times.Never);

            _bus.Verify(x => x.Publish(It.Is<CreatedAccountEvent>(a => a.AccountID == newaccountId), CancellationToken.None), Times.Never);

            _bus.Verify(x => x.Publish(It.Is<DomainNotification>(n => n.Key == ValidationErrorCodes.BetweenInterests), CancellationToken.None));

            Assert.Equal(newaccountId, Guid.Empty);
        }
    }
}
