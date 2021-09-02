using BankAccountManagementApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.EventHandler
{
    public class BankEventHandler : INotificationHandler<BankCreatedEvent>,
        INotificationHandler<ChargedInterestsEvent>
    {
        public Task Handle(BankCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ChargedInterestsEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
