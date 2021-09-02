using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Events
{
    public class CreatedAccountEvent : INotification
    {
        public Guid AccountID { get; set; }
    }
}
