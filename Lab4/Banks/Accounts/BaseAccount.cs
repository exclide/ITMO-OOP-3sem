using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

public abstract class BaseAccount : IAccount
{
    private const int YearsTillValid = 2;
    private readonly ICollection<ITransaction> _transactionHistory;
    private decimal _balance;
    private decimal _interestAmount;
    protected BaseAccount(Client client, Bank bank, int accountId, DateOnly date)
    {
        _balance = 0;
        _interestAmount = 0;
        Client = client;
        Bank = bank;
        AccountId = accountId;
        CreatedOn = date;
        LastInterest = CreatedOn;
        _transactionHistory = new List<ITransaction>();
    }

    public Client Client { get; }
    public Bank Bank { get; }

    public decimal InterestAmount
    {
        get => _interestAmount;
        set
        {
            if (value < 0)
            {
                throw new BankException("Interest can't be negative");
            }

            _interestAmount = value;
        }
    }

    public decimal Balance
    {
        get => _balance;
        set => _balance = value;
    }

    public int AccountId { get; }
    public IEnumerable<ITransaction> TransactionHistory => _transactionHistory;
    public abstract IAccountLimits AccountLimits { get; }
    public DateOnly CreatedOn { get; }
    public DateOnly LastInterest { get; set; }

    public bool IsExpired =>
        (LastInterest.DayNumber - CreatedOn.DayNumber) > 365 * YearsTillValid;

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

    public void RevertTransaction(int transactionId)
    {
        var transaction = TransactionHistory.FirstOrDefault(x => x.TransactionId == transactionId);
        if (transaction is null)
        {
            throw new BankException("Transcation not found in history");
        }

        transaction.Revert();
    }

    public void AddMonthlyInterestToBalance()
    {
        var depositTranscation = new DepositTranscation(this, InterestAmount);
        MakeTransaction(depositTranscation);
        InterestAmount = 0;
    }

    public void AddDailyInterest()
    {
        decimal dailyInterestRate = AccountLimits.AnnualInterestRate / 365 / 100;
        InterestAmount += Balance * dailyInterestRate;
    }

    public void OnNext(DateOnly value)
    {
        if (value < LastInterest)
        {
            throw new BankException("Can't time travel to the past");
        }

        while (LastInterest != value)
        {
            AddDailyInterest();

            DateOnly nextDay = LastInterest.AddDays(1);
            if (LastInterest.Month != nextDay.Month)
            {
                AddMonthlyInterestToBalance();
            }

            LastInterest = nextDay;
        }
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}