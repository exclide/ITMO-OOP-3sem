using Banks.Entities;

namespace Banks.Accounts;

/*
 Процент на остаток Balance, деньги можно всегда снимать, в минус уходить нельзя
 */

public class DebitAccount : BaseAccount
{
    public DebitAccount(Client client, Bank bank, int accountId, decimal depositAmount)
        : base(client, bank, accountId)
    {
        var accountLimits = new AccountLimits(
            false,
            0,
            bank.BankConfig.DebitAccountInterestRate,
            client.IsVerified ? 0 : bank.BankConfig.UnverifiedClientTransactionLimit);

        SetAccountLimits(accountLimits);
        Balance = depositAmount;
    }

    public override AccountType GetAccountType() => AccountType.Debit;
}