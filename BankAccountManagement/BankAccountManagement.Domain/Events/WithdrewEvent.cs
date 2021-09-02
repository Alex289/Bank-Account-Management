using MediatR;
using System;

namespace BankAccountManagementApi.Domain.Events
{
    public class WithdrewEvent : INotification
    {
        public Guid AccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
