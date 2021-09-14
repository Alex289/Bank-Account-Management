using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Notifications
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;

        public DomainNotification(string key, string value)
        {
            Initialize(key, value);
        }

        private void Initialize(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
