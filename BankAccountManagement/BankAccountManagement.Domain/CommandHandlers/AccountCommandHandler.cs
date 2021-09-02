using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Entities;
using BankAccountManagementApi.Domain.Events;
using BankAccountManagementApi.Domain.Repository;
using BankAccountManagementApi.Domain.Validations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.CommandHandlers
{
    public class AccountCommandHandler : IRequestHandler<CreateNewAccountCommand, Guid>,
        IRequestHandler<DepositCommand, decimal>,
        IRequestHandler<WithdrawCommand, decimal>
    {
        private readonly IMediator _bus;
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IMediator bus, IAccountRepository accountRepository)
        {
            _bus = bus;
            _accountRepository = accountRepository;
        }

        public async Task<Guid> Handle(CreateNewAccountCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Guid.Empty;
            }

            var account = new Account()
            {
                AccountID = Guid.NewGuid(),
                BankID = request.BankID,
                InterestLimit = request.InterestLimit,
                Interests = request.Interests,
                Money = request.Money
            };

            _accountRepository.AddAccount(account);

            await _bus.Publish(new CreatedAccountEvent() { AccountID = account.AccountID }, cancellationToken);
            return account.AccountID;
        }

        public async Task<decimal> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateDepositCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return 0;
            }

            var account = _accountRepository.GetById(request.AccountId);

            if (account != null)
            {
                account.Money += request.Amount;
                _accountRepository.SaveChanges();

                await _bus.Publish(new DepositedEvent() { AccountID = account.AccountID, Amount = account.Money }, cancellationToken);
                return account.Money;
            }

            return 0;
        }

        public async Task<decimal> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var validator = new ValidateWithdrawCommand();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return 0;
            }

            var account = _accountRepository.GetById(request.AccountId);

            if (account != null && account.Money - request.Amount > account.InterestLimit)
            {
                account.Money -= request.Amount;
                _accountRepository.SaveChanges();

                await _bus.Publish(new WithdrewEvent() { AccountID = account.AccountID, Amount = account.Money }, cancellationToken);
                return account.Money;
            }

            return 0;
        }
    }
}
