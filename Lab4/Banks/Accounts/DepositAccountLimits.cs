using Banks.Entities;

namespace Banks.Accounts;

public class DepositAccountLimits : IAccountLimits
{
    private readonly IAccount _account;
    private readonly decimal _initialDeposit;

    public DepositAccountLimits(IAccount account, decimal initialDeposit)
    {
        _account = account;
        _initialDeposit = initialDeposit;
        CanGoNegative = false;
        TransactionCommission = 0;
    }

    public bool CanGoNegative { get; }
    public decimal TransactionCommission { get; }

    public decimal AnnualInterestRate
    {
        get
        {
            if (_initialDeposit < _account.Bank.BankConfig.DepositAccountInterestRates.FirstRange)
            {
                return _account.Bank.BankConfig.DepositAccountInterestRates.FirstPercent;
            }

            if (_initialDeposit < _account.Bank.BankConfig.DepositAccountInterestRates.SecondRange)
            {
                return _account.Bank.BankConfig.DepositAccountInterestRates.SecondPercent;
            }

            return _account.Bank.BankConfig.DepositAccountInterestRates.ThirdPercent;
        }
    }

    public decimal WithdrawTransferLimit => _account.IsExpired ? decimal.MaxValue : 0;
}