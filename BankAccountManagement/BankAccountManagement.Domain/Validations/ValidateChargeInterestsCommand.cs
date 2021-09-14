using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Errors;
using FluentValidation;

namespace BankAccountManagementApi.Domain.Validations
{
    public class ValidateChargeInterestsCommand : AbstractValidator<ChargeInterestsCommand>
    {
        public ValidateChargeInterestsCommand()
        {
            AddRuleForBankId();
        }

        protected void AddRuleForBankId()
        {
            RuleFor(cmd => cmd.BankId)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyBankId);
        }
    }
}
