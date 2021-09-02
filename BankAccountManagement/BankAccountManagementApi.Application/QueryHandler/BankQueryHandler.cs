using BankAccountManagementApi.Application.Queries.Bank;
using BankAccountManagementApi.Application.ViewModels;
using BankAccountManagementApi.Domain.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Application.QueryHandler
{
    public class BankQueryHandler : IRequestHandler<GetAllBanksQuery, List<BankListViewModel>>
    {
        private readonly IBankRepository _bankRepository;

        public BankQueryHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<List<BankListViewModel>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken)
        {
            List<BankListViewModel> bankListQuery = _bankRepository.GetAll()
                .Select(item => new BankListViewModel
                {
                    BankID = item.BankID,
                    BankName = item.BankName
                })
                .ToList();

            return bankListQuery;
        }
    }
}
