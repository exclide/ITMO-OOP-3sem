using Banks.Entities;

namespace Banks.Accounts;

public class DebitAccount : BaseAccount
{
    public DebitAccount(Client client, Bank bank, int accountId, DateOnly date, decimal depositAmount)
        : base(client, bank, accountId, date)
    {
        var accountLimits = new AccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override AccountType AccountType => AccountType.Debit;
}