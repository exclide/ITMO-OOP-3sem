namespace Banks.Accounts;

public class AccountLimits
{
    public AccountLimits(
        bool canGoNegative,
        decimal transactionComission,
        decimal annualInterestRate,
        decimal withdrawTransferLimit)
    {
        CanGoNegative = canGoNegative;
        TransactionComission = transactionComission;
        AnnualInterestRate = annualInterestRate;
        WithdrawTransferLimit = withdrawTransferLimit;
    }

    public bool CanGoNegative { get; }
    public decimal TransactionComission { get; }
    public decimal AnnualInterestRate { get; }
    public decimal WithdrawTransferLimit { get; }
}