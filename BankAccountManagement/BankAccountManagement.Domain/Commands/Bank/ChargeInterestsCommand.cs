using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Commands.Bank
{
    public class ChargeInterestsCommand : IRequest<int>
    {
        public Guid BankId;
        public ChargeInterestsCommand(Guid bankId)
        {
            BankId = bankId;
        }
    }
}
