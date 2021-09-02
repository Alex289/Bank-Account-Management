using BankAccountManagementApi.Application.ViewModels;
using MediatR;
using System;

namespace BankAccountManagementApi.Application.Queries.Account
{
    public class GetAccountsByIdQuery : IRequest<AccountListViewModel>
    {
        public Guid AccountID { get; set; }

        public GetAccountsByIdQuery(Guid accountID)
        {
            AccountID = accountID;
        }
    }
}
