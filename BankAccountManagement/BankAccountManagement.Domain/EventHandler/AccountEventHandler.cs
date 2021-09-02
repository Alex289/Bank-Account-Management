using BankAccountManagementApi.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.EventHandler
{
    public class AccountEventHandler : INotificationHandler<CreatedAccountEvent>,
        INotificationHandler<DepositedEvent>,
        INotificationHandler<WithdrewEvent>
    {
        public Task Handle(CreatedAccountEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(DepositedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(WithdrewEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
