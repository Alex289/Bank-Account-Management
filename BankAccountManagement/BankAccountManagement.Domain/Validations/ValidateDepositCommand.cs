using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Errors;
using FluentValidation;

namespace BankAccountManagementApi.Domain.Validations
{
    public class ValidateDepositCommand : AbstractValidator<DepositCommand>
    {
        public ValidateDepositCommand()
        {
            AddRuleForAccountId();
            AddRuleForAmount();
        }

        protected void AddRuleForAccountId()
        {
            RuleFor(cmd => cmd.AccountId)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyAccountId);
        }

        protected void AddRuleForAmount()
        {
            RuleFor(cmd => cmd.Amount)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyAmount)
                .GreaterThan(0)
                .WithErrorCode(ValidationErrorCodes.TooLowAmount);
        }
    }
}
