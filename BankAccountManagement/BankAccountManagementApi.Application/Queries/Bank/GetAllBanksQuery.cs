using BankAccountManagementApi.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace BankAccountManagementApi.Application.Queries.Bank
{
    public class GetAllBanksQuery : IRequest<List<BankListViewModel>>
    {
    }
}
