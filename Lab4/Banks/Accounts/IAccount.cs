using Banks.Commands;

namespace Banks.Accounts;

public interface IAccount
{
    public int AccountId { get; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; }
    public AccountLimits AccountLimits { get; set; }
    void MakeTransaction(ITransaction transaction);
    void RevertTransaction(ITransaction transaction);
}