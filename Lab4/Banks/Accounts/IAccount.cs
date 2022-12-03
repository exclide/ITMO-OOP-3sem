using Banks.Commands;
using Banks.Entities;

namespace Banks.Accounts;

public interface IAccount : IObserver<DateOnly>
{
    Client Client { get; }
    Bank Bank { get; }
    int AccountId { get; }
    decimal Balance { get; set; }
    bool IsExpired { get; }
    IEnumerable<ITransaction> TransactionHistory { get; }
    DateOnly CreatedOn { get; }
    DateOnly LastInterest { get; set; }
    decimal InterestAmount { get; set; }
    IAccountLimits AccountLimits { get; }
    void MakeTransaction(ITransaction transaction);
    void RevertTransaction(ITransaction transaction);
    void RevertTransaction(int transactionId);
    void AddDailyInterest();
    void AddMonthlyInterestToBalance();
}