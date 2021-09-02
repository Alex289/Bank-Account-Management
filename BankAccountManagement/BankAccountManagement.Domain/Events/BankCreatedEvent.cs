using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Events
{
    public class BankCreatedEvent : INotification
    {
        public Guid BankID { get; set; }
    }
}
