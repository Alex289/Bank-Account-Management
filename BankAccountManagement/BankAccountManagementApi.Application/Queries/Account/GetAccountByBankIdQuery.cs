using BankAccountManagementApi.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace BankAccountManagementApi.Application.Queries.Account
{
    public class GetAccountByBankIdQueryAsync : IRequest<List<AccountListViewModel>>
    {
        public Guid BankId { get; set; }

        public GetAccountByBankIdQueryAsync(Guid bankId)
        {
            BankId = bankId;
        }
    }
}
