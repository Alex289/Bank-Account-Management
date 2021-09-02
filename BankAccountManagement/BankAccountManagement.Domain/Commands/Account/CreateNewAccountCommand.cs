using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Commands.Account
{
    public class CreateNewAccountCommand : IRequest<Guid>
    {
        public decimal Money { get; set; }
        public int InterestLimit { get; set; }
        public double Interests { get; set; }
        public Guid BankID { get; set; }

        public CreateNewAccountCommand(decimal money, int interestLimit, double interests, Guid bankID)
        {
            Money = money;
            InterestLimit = interestLimit;
            Interests = interests;
            BankID = bankID;
        }
    }
}
