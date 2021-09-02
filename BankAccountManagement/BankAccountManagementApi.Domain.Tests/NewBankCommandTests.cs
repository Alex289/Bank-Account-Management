using BankAccountManagementApi.Domain.CommandHandlers;
using BankAccountManagementApi.Domain.Commands.Bank;
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
    public class NewBankCommandTests
    {
        private readonly Mock<IMediator> _bus;
        private readonly BankCommandHandler _bankCommandHandler;
        private readonly Mock<IBankRepository> _bankRepository;
        private readonly Mock<IAccountRepository> _accountRepository;

        public NewBankCommandTests()
        {
            _bus = new Mock<IMediator>();
            _bankRepository = new Mock<IBankRepository>();
            _accountRepository = new Mock<IAccountRepository>();

            _bankCommandHandler = new BankCommandHandler(_bankRepository.Object, _accountRepository.Object, _bus.Object);
        }

        [Fact]
        public async Task Should_Create_New_Bank()
        {
            // Arrange
            var newBankCommand = new CreateNewBankCommand(Guid.NewGuid(), "test");

            // Act
            var newbankId = await _bankCommandHandler.Handle(newBankCommand, CancellationToken.None);

            // Assert
            _bankRepository.Verify(x => x.AddBank(It.Is<Bank>(b => b.BankID == newBankCommand.BankID && b.BankName == newBankCommand.BankName)));

            _bus.Verify(x => x.Publish(It.Is<BankCreatedEvent>(b => b.BankID == newBankCommand.BankID), CancellationToken.None));

            Assert.Equal(newBankCommand.BankID, newbankId);
        }

        [Fact]
        public async Task Invalid_Name_Length()
        {
            // Arrange
            var newBankCommand = new CreateNewBankCommand(Guid.NewGuid(), "t8SgCli9gF1B3j3mrQnWEimYhYJkFNunqyD86jB9OBdox58iD8T");

            // Act
            var newbankId = await _bankCommandHandler.Handle(newBankCommand, CancellationToken.None);

            // Assert
            _bankRepository.Verify(x => x.AddBank(It.Is<Bank>(b => b.BankID == newBankCommand.BankID && b.BankName == newBankCommand.BankName)), Times.Never);

            _bus.Verify(x => x.Publish(It.Is<BankCreatedEvent>(b => b.BankID == newBankCommand.BankID), CancellationToken.None), Times.Never);

            Assert.Equal(Guid.Empty, newbankId);
        }
    }
}
