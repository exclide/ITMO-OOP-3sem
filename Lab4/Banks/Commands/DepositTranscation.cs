using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Commands;

public class DepositTranscation : ITransaction
{
    private readonly IAccount _account;
    private readonly decimal _depositAmount;
    private readonly AccountLimits _accountLimits;
    private readonly decimal _beforeBalance;
    private bool _hasRun;

    public DepositTranscation(IAccount account, decimal depositAmount)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (depositAmount < 0)
        {
            throw new BankException("Deposit amount can't be negative");
        }

        _account = account;
        _depositAmount = depositAmount;
        _accountLimits = account.AccountLimits;
        _beforeBalance = account.Balance;
        _hasRun = false;
        TransactionId = ITransaction.transactionCounter++;
    }

    public int TransactionId { get; }

    public void Run()
    {
        if (_hasRun)
        {
            throw new BankException("Transaction was already executed");
        }

        decimal newBalance = _beforeBalance + _depositAmount;

        _account.Balance = newBalance;
        _hasRun = true;
    }

    public void Revert()
    {
        if (!_hasRun)
        {
            throw new BankException("Transaction wasn't executed or was already reverted");
        }

        decimal currentBalance = _account.Balance;
        decimal newBalance = currentBalance - _depositAmount;

        _account.Balance = newBalance;
        _hasRun = false;
    }

    public override string ToString()
    {
        return $"{TransactionId}. Type: Deposit, Balance before: {_beforeBalance}, Deposit amount: {_depositAmount}, Acc ID: {_account.AccountId}";
    }
}