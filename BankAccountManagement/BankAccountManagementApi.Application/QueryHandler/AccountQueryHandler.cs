using BankAccountManagementApi.Application.Queries.Account;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.QueryHandler
{
    public class AccountQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountListViewModel>>,
        IRequestHandler<GetAccountsByIdQuery, AccountListViewModel>,
        IRequestHandler<GetAccountByBankIdQueryAsync, List<AccountListViewModel>>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountListViewModel>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var list = _accountRepository.GetAll()
                .Select(item => new AccountListViewModel
                {
                    AccountID = item.AccountID,
                    Money = item.Money,
                    InterestLimit = item.InterestLimit,
                    Interests = item.Interests,
                    BankID = item.BankID
                })
                .ToList();

            return list;
        }

        public async Task<AccountListViewModel> Handle(GetAccountsByIdQuery request, CancellationToken cancellationToken)
        {
            var acc = _accountRepository.GetAll()
                .Where(item => item.AccountID == request.AccountID)
                .Select(item => new AccountListViewModel
                {
                    AccountID = item.AccountID,
                    Money = item.Money,
                    InterestLimit = item.InterestLimit,
                    Interests = item.Interests,
                    BankID = item.BankID
                })
                .FirstOrDefault();

            return acc;
        }

        public async Task<List<AccountListViewModel>> Handle(GetAccountByBankIdQueryAsync request, CancellationToken cancellationToken)
        {
            var list = _accountRepository.GetAll()
                .Where(item => item.BankID == request.BankId)
                .Select(item => new AccountListViewModel
                {
                    AccountID = item.AccountID,
                    Money = item.Money,
                    InterestLimit = item.InterestLimit,
                    Interests = item.Interests,
                    BankID = item.BankID
                })
                .ToList();

            return list;
        }
    }
}
