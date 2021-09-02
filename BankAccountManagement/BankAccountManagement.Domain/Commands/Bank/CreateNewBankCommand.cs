using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Commands.Bank
{
    public class CreateNewBankCommand : IRequest<Guid>
    {
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public CreateNewBankCommand(Guid bankID, string bankName)
        {
            BankID = bankID;
            BankName = bankName;
        }
    }
}
