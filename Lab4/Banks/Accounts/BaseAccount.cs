using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

public abstract class BaseAccount : IAccount
{
    private readonly ICollection<ITransaction> _transactionHistory;
    private decimal _balance;
    protected BaseAccount(Client client, Bank bank, int accountId)
    {
        Balance = 0;
        Client = client;
        Bank = bank;
        AccountId = accountId;
        _transactionHistory = new List<ITransaction>();
    }

    public Client Client { get; }
    public Bank Bank { get; }

    public decimal Balance
    {
        get => _balance;
        set
        {
            if (!AccountLimits.CanGoNegative && value < 0)
            {
                throw new BankException("Can't set negative balance on this account");
            }

            _balance = value;
        }
    }

    public int AccountId { get; }
    public IEnumerable<ITransaction> TransactionHistory => _transactionHistory;
    public abstract AccountType AccountType { get; }
    public AccountLimits AccountLimits { get; set; }

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
}