using Banks.Entities;

namespace Banks.Accounts;

public class CreditAccount : BaseAccount
{
    public CreditAccount(Client client, Bank bank, int accountId, DateOnly date, decimal depositAmount)
        : base(client, bank, accountId, date)
    {
        var accountLimits = new CreditAccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override string ToString()
    {
        return
            $"{AccountId}. Type: Credit, Balance: {Balance}, InterestBalance {InterestAmount}, Created on: {CreatedOn}, " +
            $"Bank: {Bank.BankName}, Client: {Client.ClientName}";
    }
}