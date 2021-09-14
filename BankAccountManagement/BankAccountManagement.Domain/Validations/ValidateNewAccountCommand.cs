using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Errors;
using FluentValidation;

namespace BankAccountManagementApi.Domain.Validations
{
    public class ValidateNewAccountCommand : AbstractValidator<CreateNewAccountCommand>
    {
        public ValidateNewAccountCommand()
        {
            AddRuleForBankId();
            AddRuleForInterestLimit();
            AddRuleForInterests();
        }

        protected void AddRuleForBankId()
        {
            RuleFor(cmd => cmd.BankID)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.EmptyBankId);
        }

        protected void AddRuleForInterestLimit()
        {
            RuleFor(cmd => cmd.InterestLimit)
                .NotEmpty()
                .Unless(cmd => cmd.Interests == 0)
                .WithErrorCode(ValidationErrorCodes.EmptyInterestLimit)
                .InclusiveBetween(-1000, 0)
                .WithErrorCode(ValidationErrorCodes.BetweenInterestLimit);
        }

        protected void AddRuleForInterests()
        {
            RuleFor(cmd => cmd.Interests)
                .NotEmpty()
                .Unless(cmd => cmd.Interests == 0)
                .WithErrorCode(ValidationErrorCodes.EmptyInterests)
                .InclusiveBetween(0, 15)
                .WithErrorCode(ValidationErrorCodes.BetweenInterests);
        }
    }
}