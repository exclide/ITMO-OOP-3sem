namespace Banks.Accounts;

public interface IAccountLimits
{
    bool CanGoNegative { get; }
    decimal TransactionCommission { get; }
    decimal AnnualInterestRate { get; }
    decimal WithdrawTransferLimit { get; }
}