using Banks.Entities;

namespace Banks.Accounts;

public class CreditAccount : BaseAccount
{
    public CreditAccount(Client client, Bank bank, int accountId, decimal depositAmount)
        : base(client, bank, accountId)
    {
        var accountLimits = new AccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override AccountType AccountType => AccountType.Credit;
}