using BankAccountManagementApi.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankAccountManagementApi.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        public ApiController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool IsValidOperation() 
        {
            return (!_notifications.HasNotifications());
        }

        protected IActionResult CreateResponse<T>(T result)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(
                new
                {
                    success = false,
                    code = _notifications.GetNotifications().Select(n => n.Key),
                    errors = _notifications.GetNotifications().Select(n => n.Value)
                });
        }
    }
}
