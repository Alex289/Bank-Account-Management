using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagementApi.Domain.Errors
{
    public static class ValidationErrorCodes
    {
        // Charge interests
        public const string ChargeInterestsInvalidBankId = "CHARGE_INTERESTS_INVALID_BANK_ID";

        // Deposit
        public const string DepositInvalidAccountId = "DEPOSIT_INVALID_ACCOUNT_ID";
        public const string DepositInvalidAmount = "DEPOSIT_INVALID_AMOUNT";

        // New account
        public const string NewAccountInvalidBankId = "CREATE_NEW_ACCOUNT_INVALID_BANK_ID";
        public const string NewAccountInvalidInterestLimit = "CREATE_NEW_ACCOUNT_INVALID_INTEREST_LIMIT";
        public const string NewAccountInvalidInterests = "CREATE_NEW_ACCOUNT_INVALID_INTERESTS";

        // New bank
        public const string NewBankInvalidBankId = "NEW_BANK_INVALID_BANK_ID";
        public const string NewBankInvalidBankName = "CREATE_NEW_BANK_INVALID_BANK_NAME";

        // Withdraw
        public const string WithdrawInvalidAccountId = "WITHDRAW_INVALID_BANK_ID";
        public const string WithdrawInvalidAmount = "WITHDRAW_INVALID_AMOUNT";
    }
}
