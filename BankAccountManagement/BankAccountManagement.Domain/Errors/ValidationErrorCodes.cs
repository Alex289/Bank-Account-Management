using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Errors
{
    public static class ValidationErrorCodes
    {
        // General
        public const string EmptyAccountId = "INVALID_EMPTY_ACCOUNT_ID";
        public const string EmptyBankId = "INVALID_EMPTY_BANK_ID";

        // Deposit/Withdraw
        public const string EmptyAmount = "INVALID_EMPTY_AMOUNT";
        public const string TooLowAmount = "INVALID_TOO_LOW_AMOUNT";
        public const string TooHighAmount = "INVALID_TOO_HIGH_AMOUNT";

        // New account
        public const string EmptyInterestLimit = "INVALID_EMPTY_INTEREST_LIMIT";
        public const string BetweenInterestLimit = "INVALID_INTEREST_LIMIT_BETWEEN_0_AND_-1000";

        public const string EmptyInterests = "INVALID_EMPTY_INTERESTS";
        public const string BetweenInterests = "INVALID_INTERESTS_BETWEEN_0_AND_15";
        // New bank
        public const string EmptyBankName = "INVALID_EMPTY_BANK_NAME";
        public const string TooManyCharacters = "INVALID_TOO_MANY_CHARACTERS_BANK_NAME";
    }
}
