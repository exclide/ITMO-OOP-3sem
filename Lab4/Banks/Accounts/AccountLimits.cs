using Banks.Entities;

namespace Banks.Accounts;

public class AccountLimits
{
    public AccountLimits(IAccount account, decimal initialDeposit)
    {
        Account = account;
        InitialDeposit = initialDeposit;
    }

    public IAccount Account { get; }
    public decimal InitialDeposit { get; }
    public bool CanGoNegative => Account.AccountType == AccountType.Credit;

    public decimal TransactionComission =>
        Account.AccountType == AccountType.Credit ? Account.Bank.BankConfig.CreditAccountCommissionFixed : 0;

    public decimal AnnualInterestRate
    {
        get
        {
            if (Account.AccountType == AccountType.Debit)
            {
                return Account.Bank.BankConfig.DebitAccountInterestRate;
            }

            if (Account.AccountType == AccountType.Credit)
            {
                return 0;
            }

            if (InitialDeposit < Account.Bank.BankConfig.DepositAccountInterestRates.FirstRange)
            {
                return Account.Bank.BankConfig.DepositAccountInterestRates.FirstPercent;
            }

            if (InitialDeposit < Account.Bank.BankConfig.DepositAccountInterestRates.SecondRange)
            {
                return Account.Bank.BankConfig.DepositAccountInterestRates.SecondPercent;
            }

            return Account.Bank.BankConfig.DepositAccountInterestRates.ThirdPercent;
        }
    }

    public decimal WithdrawTransferLimit
    {
        get
        {
            if (Account.AccountType == AccountType.Deposit)
            {
                return Account.IsExpired ? decimal.MaxValue : 0;
            }

            return Account.Client.IsVerified
                ? decimal.MaxValue
                : Account.Bank.BankConfig.UnverifiedClientTransactionLimit;
        }
    }
}