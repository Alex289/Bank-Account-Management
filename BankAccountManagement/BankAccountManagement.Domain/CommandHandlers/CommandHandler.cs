using BankAccountManagementApi.Domain.Interfaces;
using BankAccountManagementApi.Domain.Notifications;
using FluentValidation.Results;
using MediatR;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediator _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;

        public CommandHandler(
            IMediator bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        public async Task<bool> CommitAsync()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }

            if (await _uow.CommitAsync())
            {
                return true;
            }

            await _bus.Publish(
                new DomainNotification(
                    "Commit",
                    "Problem saving data. Please try again."));
            return false;
        }

        protected async Task NotifyValidationErrorsAsync(ValidationResult message)
        {
            if (message == null)
            {
                return;
            }

            foreach (ValidationFailure error in message.Errors)
            {
                await _bus.Publish(
                    new DomainNotification(
                        error.ErrorCode,
                        error.ErrorMessage));
            }
        }
    }
}
