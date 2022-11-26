using Banks.Entities;

namespace Banks.Accounts;

public class CreditAccountLimits : IAccountLimits
{
    private readonly IAccount _account;
    private readonly decimal _initialDeposit;

    public CreditAccountLimits(IAccount account, decimal initialDeposit)
    {
        _account = account;
        _initialDeposit = initialDeposit;
        CanGoNegative = true;
        AnnualInterestRate = 0;
    }

    public bool CanGoNegative { get; }
    public decimal TransactionCommission => _account.Bank.BankConfig.CreditAccountCommissionFixed;
    public decimal AnnualInterestRate { get; }
    public decimal WithdrawTransferLimit => _account.Client.IsVerified ? 0 : _account.Bank.BankConfig.UnverifiedClientTransactionLimit;
}