using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Accounts;

public abstract class BaseAccount : IAccount
{
    private readonly ICollection<ITransaction> _transactionHistory;
    private AccountLimits _accountLimits;
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
            if (!_accountLimits.CanGoNegative && value < 0)
            {
                throw new BankException("Can't set negative balance on this account");
            }

            _balance = value;
        }
    }

    public int AccountId { get; }
    public IEnumerable<ITransaction> TransactionHistory => _transactionHistory;

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

    public AccountLimits GetAccountLimits() => _accountLimits;

    public void SetAccountLimits(AccountLimits accountLimits)
    {
        _accountLimits = accountLimits;
    }

    public abstract AccountType GetAccountType();
}