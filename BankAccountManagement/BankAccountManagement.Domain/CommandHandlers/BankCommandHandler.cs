using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Repository;
using BankAccountManagementApi.Domain.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.CommandHandlers
{
    public class BankCommandHandler : IRequestHandler<CreateNewBankCommand, Guid>,
        IRequestHandler<ChargeInterestsCommand, int>
    {
        private readonly IMediator _bus;
        private readonly IBankRepository _bankRepository;
        private readonly IAccountRepository _accountRepository;

        public BankCommandHandler(IBankRepository bankRepository, IAccountRepository accountRepository, IMediator bus)
        {
            _bankRepository = bankRepository;
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public async Task<Guid> Handle(CreateNewBankCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateNewBankCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Guid.Empty;
            }

            var bank = new Bank() { BankID = request.BankID, BankName = request.BankName };
            _bankRepository.AddBank(bank);
            await _bus.Publish(new BankCreatedEvent() { BankID = bank.BankID }, cancellationToken);
            return bank.BankID;
        }

        public async Task<int> Handle(ChargeInterestsCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateChargeInterestsCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return 0;
            }

            int count = 0;
            List<Account> accList = _accountRepository.GetByBankId(request.BankId);

            foreach (Account acc in accList)
            {
                if (acc.Money < 0)
                {
                    acc.Money += acc.Money * Convert.ToDecimal(acc.Interests);
                    count++;
                }
            }

            _accountRepository.SaveChanges();

            await _bus.Publish(new ChargedInterestsEvent { ChargedAccountsNumber = count }, cancellationToken);
            return count;
        }
    }
}
