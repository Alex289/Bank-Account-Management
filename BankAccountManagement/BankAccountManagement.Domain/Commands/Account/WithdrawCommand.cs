using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Commands.Account
{
    public class WithdrawCommand : IRequest<decimal>
    {
        public Guid AccountId;
        public decimal Amount;
        public WithdrawCommand(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
