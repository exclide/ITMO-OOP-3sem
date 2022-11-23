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
        var accountLimits = new AccountLimits(
            true,
            bank.BankConfig.CreditAccountCommissionFixed,
            0,
            client.IsVerified ? 0 : bank.BankConfig.UnverifiedClientTransactionLimit);

        SetAccountLimits(accountLimits);
        Balance = depositAmount;
    }

    public override AccountType GetAccountType() => AccountType.Credit;
}