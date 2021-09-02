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
                .WithErrorCode(ValidationErrorCodes.DepositInvalidAccountId);
        }

        protected void AddRuleForAmount()
        {
            RuleFor(cmd => cmd.Amount)
                .NotEmpty()
                .GreaterThan(0)
                .WithErrorCode(ValidationErrorCodes.DepositInvalidAmount);
        }
    }
}
