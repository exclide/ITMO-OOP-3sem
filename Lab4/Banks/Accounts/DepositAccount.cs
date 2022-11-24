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
        var accountLimits = new AccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override AccountType AccountType => AccountType.Deposit;
}