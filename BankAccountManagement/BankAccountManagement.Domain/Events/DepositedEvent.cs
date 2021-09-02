using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Events
{
    public class DepositedEvent : INotification
    {
        public Guid AccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
