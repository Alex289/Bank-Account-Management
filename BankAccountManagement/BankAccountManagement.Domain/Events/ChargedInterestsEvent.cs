using MediatR;

namespace BankAccountManagementApi.Domain.Events
{
    public class ChargedInterestsEvent : INotification
    {
        public int ChargedAccountsNumber { get; set; }
    }
}
