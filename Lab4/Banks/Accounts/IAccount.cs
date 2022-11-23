using Banks.Commands;

namespace Banks.Accounts;

public interface IAccount
{
    public int AccountId { get; }
    public decimal Balance { get; set; }
    void MakeTransaction(ITransaction transaction);
    void RevertTransaction(ITransaction transaction);
    AccountType GetAccountType();
    AccountLimits GetAccountLimits();
    void SetAccountLimits(AccountLimits accountLimits);
}