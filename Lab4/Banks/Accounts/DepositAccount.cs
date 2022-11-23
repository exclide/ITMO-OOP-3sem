using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

/*
 Процент на остаток Balance в зависимости от начальной суммы,
 деньги снимать нельзя, пока не закончится срок счета,
 пополнять можно
 */

public class DepositAccount : BaseAccount
{
    public DepositAccount(Client client, Bank bank, int accountId, decimal depositAmount)
        : base(client, bank, accountId)
    {
        decimal depositInterestRate = 0;
        if (depositAmount > bank.BankConfig.DepositAccountInterestRates.SecondRange)
        {
            depositInterestRate = bank.BankConfig.DepositAccountInterestRates.ThirdPercent;
        }
        else if (depositAmount > bank.BankConfig.DepositAccountInterestRates.FirstRange)
        {
            depositInterestRate = bank.BankConfig.DepositAccountInterestRates.SecondPercent;
        }
        else
        {
            depositInterestRate = bank.BankConfig.DepositAccountInterestRates.FirstPercent;
        }

        var accountLimits = new AccountLimits(
            false,
            0,
            depositInterestRate,
            0);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override AccountType AccountType => AccountType.Deposit;
}