using Banks.Entities;

namespace Banks.Accounts;

public class DebitAccountLimits : IAccountLimits
{
    private readonly IAccount _account;
    private readonly decimal _initialDeposit;

    public DebitAccountLimits(IAccount account, decimal initialDeposit)
    {
        _account = account;
        _initialDeposit = initialDeposit;
        CanGoNegative = false;
        TransactionCommission = 0;
    }

    public bool CanGoNegative { get; }
    public decimal TransactionCommission { get; }
    public decimal AnnualInterestRate => _account.Bank.BankConfig.DebitAccountInterestRate;
    public decimal WithdrawTransferLimit => _account.Client.IsVerified ? 0 : _account.Bank.BankConfig.UnverifiedClientTransactionLimit;
}