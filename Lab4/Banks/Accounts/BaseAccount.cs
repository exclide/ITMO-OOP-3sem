using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

public abstract class BaseAccount : IAccount
{
    private const int YearsTillValid = 5;
    private readonly ICollection<ITransaction> _transactionHistory;
    private decimal _balance;
    protected BaseAccount(Client client, Bank bank, int accountId)
    {
        Balance = 0;
        Client = client;
        Bank = bank;
        AccountId = accountId;
        CreatedOn = DateOnly.FromDateTime(DateTime.Now);
        _transactionHistory = new List<ITransaction>();
    }

    public Client Client { get; }
    public Bank Bank { get; }
    public decimal InterestAmount { get; set; }

    public decimal Balance
    {
        get => _balance;
        set => _balance = value;
    }

    public int AccountId { get; }
    public IEnumerable<ITransaction> TransactionHistory => _transactionHistory;
    public abstract AccountType AccountType { get; }
    public AccountLimits AccountLimits { get; set; }
    public DateOnly CreatedOn { get; }

    public bool IsExpired =>
        (DateOnly.FromDateTime(DateTime.Now).DayNumber - CreatedOn.DayNumber) > 365 * YearsTillValid;

    public void MakeTransaction(ITransaction transaction)
    {
        transaction.Run();
        _transactionHistory.Add(transaction);
    }

    public void RevertTransaction(ITransaction transaction)
    {
        if (!TransactionHistory.Contains(transaction))
        {
            throw new BankException("Transcation not found in history");
        }

        transaction.Revert();
    }

    public void AddInterestToBalance()
    {
        Balance += InterestAmount;
        InterestAmount = 0;
    }

    public void AddCurrentInterest(DateOnly time)
    {
    }
}