using Banks.Entities;

namespace Banks.Accounts;

public class DebitAccount : BaseAccount
{
    public DebitAccount(Client client, Bank bank, int accountId, DateOnly date, decimal depositAmount)
        : base(client, bank, accountId, date)
    {
        var accountLimits = new DebitAccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override string ToString()
    {
        return
            $"{AccountId}. Type: Debit, Balance: {Balance}, InterestBalance {InterestAmount}, Created on: {CreatedOn}, " +
            $"Bank: {Bank.BankName}, Client: {Client.ClientName}";
    }
}