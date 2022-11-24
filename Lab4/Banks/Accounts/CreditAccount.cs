using Banks.Entities;

namespace Banks.Accounts;

/*
деньги можно всегда снимать,
можно в минус,
фиксированная комиссия на каждую транзу, если в минусе
*/

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