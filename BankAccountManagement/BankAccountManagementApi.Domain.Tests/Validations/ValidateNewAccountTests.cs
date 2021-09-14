using BankAccountManagementApi.Domain.Commands.Account;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Validations;
using System;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests.Validations
{
    public class ValidateNewAccountTests
    {
        [Fact]
        public void Should_Not_Validate_Invalid_Empty_Guid()
        {
            var createCommand = new CreateNewAccountCommand(10, -111, 1, Guid.Empty);
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.EmptyBankId, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_Low_Interest_Limit()
        {
            var createCommand = new CreateNewAccountCommand(10, -1001, 1, Guid.NewGuid());
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.BetweenInterestLimit, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_High_Interest_Limit()
        {
            var createCommand = new CreateNewAccountCommand(10, 1, 1, Guid.NewGuid());
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.BetweenInterestLimit, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_Low_Interests()
        {
            var createCommand = new CreateNewAccountCommand(10, -111, -1, Guid.NewGuid());
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);
         
            Assert.Equal(ValidationErrorCodes.BetweenInterests, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_High_Interests()
        {
            var createCommand = new CreateNewAccountCommand(10, -111, 16, Guid.NewGuid());
            var validator = new ValidateNewAccountCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);
         
            Assert.Equal(ValidationErrorCodes.BetweenInterests, validationResult.Errors[0].ErrorCode);
        }
    }
}
