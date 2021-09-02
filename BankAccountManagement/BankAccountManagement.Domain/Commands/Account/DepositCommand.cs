using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Commands.Account
{
    public class DepositCommand : IRequest<decimal>
    {
        public Guid AccountId;
        public decimal Amount;
        public DepositCommand(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
