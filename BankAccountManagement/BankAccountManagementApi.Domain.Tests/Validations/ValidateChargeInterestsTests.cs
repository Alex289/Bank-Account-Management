using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Validations;
using System;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests.Validations
{
    public class ValidateChargeInterestsTests
    {
        [Fact]
        public void Should_Not_Validate_Invalid_Empty_Guid()
        {
            var chargeInterestsCommand = new ChargeInterestsCommand(Guid.Empty);
            var validator = new ValidateChargeInterestsCommand();
            var validationResult = validator.Validate(chargeInterestsCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.ChargeInterestsInvalidBankId, validationResult.Errors[0].ErrorCode);
        }
    }
}
