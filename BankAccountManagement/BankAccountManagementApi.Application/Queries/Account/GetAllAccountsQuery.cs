using BankAccountManagementApi.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace BankAccountManagementApi.Application.Queries.Account
{
    public class GetAllAccountsQuery : IRequest<List<AccountListViewModel>>
    {
    }
}
