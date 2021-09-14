using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Errors;
using FluentValidation;

namespace BankAccountManagementApi.Domain.Validations
{
    public class ValidateNewBankCommand : AbstractValidator<CreateNewBankCommand>
    {
        public ValidateNewBankCommand()
        {
            AddRuleForBankId();
            AddRuleForBankName();
        }

        protected void AddRuleForBankId()
        {
            RuleFor(cmd => cmd.BankID)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyBankId);
        }

        protected void AddRuleForBankName()
        {
            RuleFor(cmd => cmd.BankName)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyBankName)
                .MaximumLength(50)
                .WithErrorCode(ValidationErrorCodes.TooManyCharacters);
        }
    }
}
