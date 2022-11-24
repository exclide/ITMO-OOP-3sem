using Banks.Commands;
using Banks.Entities;

namespace Banks.Accounts;

public interface IAccount
{
    Client Client { get; }
    Bank Bank { get; }
    int AccountId { get; }
    decimal Balance { get; set; }
    bool IsExpired { get; }
    DateOnly CreatedOn { get; }
    decimal InterestAmount { get; set; }
    AccountType AccountType { get; }
    AccountLimits AccountLimits { get; set; }
    void MakeTransaction(ITransaction transaction);
    void RevertTransaction(ITransaction transaction);
    void AddCurrentInterest(DateOnly time);
    void AddInterestToBalance();
}