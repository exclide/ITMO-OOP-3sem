using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Commands;

public class WithdrawTranscation : ITransaction
{
    private readonly IAccount _account;
    private readonly decimal _withdrawAmount;
    private readonly AccountLimits _accountLimits;
    private readonly decimal _beforeBalance;
    private bool _hasRun;

    public WithdrawTranscation(IAccount account, decimal withdrawAmount)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (withdrawAmount < 0)
        {
            throw new BankException("Withdraw amount can't be negative");
        }

        _account = account;
        _withdrawAmount = withdrawAmount;
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

        if (_accountLimits.WithdrawTransferLimit < _withdrawAmount)
        {
            throw new BankException("Withdraw amount exceeds limit");
        }

        decimal newBalance = _beforeBalance - _withdrawAmount;
        if (_accountLimits.TransactionComission > 0 && _beforeBalance < 0)
        {
            newBalance -= _accountLimits.TransactionComission;
        }

        if (!_accountLimits.CanGoNegative && newBalance < 0)
        {
            throw new BankException("Balance can't go negative for this account");
        }

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
        decimal newBalance = currentBalance + _withdrawAmount;
        if (_accountLimits.TransactionComission > 0 && _beforeBalance < 0)
        {
            newBalance += _accountLimits.TransactionComission;
        }

        _account.Balance = newBalance;
        _hasRun = false;
    }

    public override string ToString()
    {
        return $"{TransactionId}. Type: Withdraw, Balance before: {_beforeBalance}, Withdraw amount: {_withdrawAmount}, Acc ID: {_account.AccountId}";
    }
}