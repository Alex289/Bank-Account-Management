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
                .WithErrorCode(ValidationErrorCodes.NewBankInvalidBankId);
        }

        protected void AddRuleForBankName()
        {
            RuleFor(cmd => cmd.BankName)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.NewBankInvalidBankName)
                .MaximumLength(50)
                .WithErrorCode(ValidationErrorCodes.NewBankInvalidBankName);
        }
    }
}
