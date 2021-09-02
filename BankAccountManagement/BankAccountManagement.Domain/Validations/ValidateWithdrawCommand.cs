using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Errors;
using FluentValidation;

namespace BankAccountManagementApi.Domain.Validations
{
    public class ValidateWithdrawCommand : AbstractValidator<WithdrawCommand>
    {
        public ValidateWithdrawCommand()
        {
            AddRuleForAccountId();
            AddRuleForAmount();
        }

        protected void AddRuleForAccountId()
        {
            RuleFor(cmd => cmd.AccountId)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.WithdrawInvalidAccountId);
        }

        protected void AddRuleForAmount()
        {
            RuleFor(cmd => cmd.Amount)
                .NotEmpty()
                .LessThan(1000)
                .GreaterThan(0)
                .WithErrorCode(ValidationErrorCodes.WithdrawInvalidAmount);
        }
    }
}
