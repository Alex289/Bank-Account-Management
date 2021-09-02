using BankAccountManagementApi.Domain.Commands.Bank;
using BankAccountManagementApi.Domain.Errors;
using BankAccountManagementApi.Domain.Validations;
using System;
using Xunit;

namespace BankAccountManagementApi.Domain.Tests.Validations
{
    public class ValidateNewBankTests
    {
        [Fact]
        public void Should_Not_Validate_Invalid_Empty_Guid()
        {
            var createCommand = new CreateNewBankCommand(Guid.Empty, "name");
            var validator = new ValidateNewBankCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.NewBankInvalidBankId, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Invalid_Empty_Name()
        {
            var createCommand = new CreateNewBankCommand(Guid.NewGuid(), "");
            var validator = new ValidateNewBankCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);

            Assert.Equal(ValidationErrorCodes.NewBankInvalidBankName, validationResult.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Not_Validate_Too_Long_Name()
        {
            var createCommand = new CreateNewBankCommand(Guid.NewGuid(), "u1Eb6agYsgudhZIU9vJS8N3jCdCs05lopC8X4s0VXdFn1Rsk3Rc");
            var validator = new ValidateNewBankCommand();
            var validationResult = validator.Validate(createCommand);

            Assert.False(validationResult.IsValid);
            
            Assert.Equal(ValidationErrorCodes.NewBankInvalidBankName, validationResult.Errors[0].ErrorCode);
        }
    }
}
