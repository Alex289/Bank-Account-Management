using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Validations;
using System;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests.Validations
{
    public class ValidateDepositTests
    {
        [Fact]
        public void Should_Not_Validate_Invalid_Empty_Guid()
        {
            var depositCommand = new DepositCommand(Guid.Empty, 1);
            var validator = new ValidateDepositCommand();
            var validationResult = validator.Validate(depositCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.EmptyAccountId, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_Low_Amount()
        {
            var depositCommand = new DepositCommand(Guid.NewGuid(), -1);
            var validator = new ValidateDepositCommand();
            var validationResult = validator.Validate(depositCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.TooLowAmount, validationResult.Errors[0].ErrorCode);
        }
    }
}
