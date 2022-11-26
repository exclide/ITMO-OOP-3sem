using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

public class DepositAccount : BaseAccount
{
    public DepositAccount(Client client, Bank bank, int accountId, DateOnly date, decimal depositAmount)
        : base(client, bank, accountId, date)
    {
        var accountLimits = new DepositAccountLimits(this, depositAmount);

        AccountLimits = accountLimits;
        Balance = depositAmount;
    }

    public override string ToString()
    {
        return
            $"{AccountId}. Type: Deposit, Balance: {Balance}, InterestBalance {InterestAmount}, Created on: {CreatedOn}, " +
            $"Bank: {Bank.BankName}, Client: {Client.ClientName}";
    }
}