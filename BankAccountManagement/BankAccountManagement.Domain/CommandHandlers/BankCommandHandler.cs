using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Interfaces.Repository;
using BankAccountManagementApi.Domain.Notifications;
using BankAccountManagementApi.Domain.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.CommandHandlers
{
    public class BankCommandHandler : CommandHandler,
        IRequestHandler<CreateNewBankCommand, Guid>,
        IRequestHandler<ChargeInterestsCommand, int>
    {
        private readonly IMediator _bus;
        private readonly IBankRepository _bankRepository;
        private readonly IAccountRepository _accountRepository;

        public BankCommandHandler(
            IMediator bus, 
            IUnitOfWork uow, 
            INotificationHandler<DomainNotification> notifications, 
            IBankRepository bankRepository, 
            IAccountRepository accountRepository) : base(bus, uow, notifications)
        {
            _bus = bus;
            _bankRepository = bankRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Guid> Handle(CreateNewBankCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateNewBankCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                await NotifyValidationErrorsAsync(validationResult);
                return Guid.Empty;
            }

            var bank = new Bank() { BankID = request.BankID, BankName = request.BankName };
            await _bankRepository.AddBankAsync(bank);

            if (await CommitAsync())
            {
                await _bus.Publish(new BankCreatedEvent() { BankID = bank.BankID }, cancellationToken);
            }

            return bank.BankID;
        }

        public async Task<int> Handle(ChargeInterestsCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateChargeInterestsCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                await NotifyValidationErrorsAsync(validationResult);
                return 0;
            }

            int count = 0;
            List<Account> accList = await _accountRepository.GetByBankIdAsync(request.BankId);

            foreach (Account acc in accList)
            {
                if (acc.Money < 0)
                {
                    acc.Money += acc.Money * Convert.ToDecimal(acc.Interests);
                    count++;
                }
            }

            if (await CommitAsync())
            {
                await _bus.Publish(new ChargedInterestsEvent { ChargedAccountsNumber = count }, cancellationToken);
            }

            return count;
        }
    }
}
