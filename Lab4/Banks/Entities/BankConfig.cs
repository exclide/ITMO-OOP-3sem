using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class BankConfig
{
    public BankConfig(
        decimal unverifiedClientTransactionLimit,
        decimal creditAccountCommission,
        decimal debitAccountRate,
        DepositAccountInterestRates depositAccountInterestRates)
    {
        ArgumentNullException.ThrowIfNull(depositAccountInterestRates);
        if (unverifiedClientTransactionLimit < 0 || creditAccountCommission < 0 || debitAccountRate < 0)
        {
            throw new BankException("Invalid bank limits");
        }

        UnverifiedClientTransactionLimit = unverifiedClientTransactionLimit;
        CreditAccountCommissionFixed = creditAccountCommission;
        DepositAccountInterestRates = depositAccountInterestRates;
        DebitAccountInterestRate = debitAccountRate;
    }

    public decimal UnverifiedClientTransactionLimit { get; set; }
    public decimal CreditAccountCommissionFixed { get; set; }
    public decimal DebitAccountInterestRate { get; }
    public DepositAccountInterestRates DepositAccountInterestRates { get; set; }

    public override string ToString()
    {
        return $"{nameof(UnverifiedClientTransactionLimit)}: {UnverifiedClientTransactionLimit}, " +
               $"{nameof(CreditAccountCommissionFixed)}: {CreditAccountCommissionFixed}, " +
               $"{nameof(DebitAccountInterestRate)}: {DebitAccountInterestRate}, " +
               $"{nameof(DepositAccountInterestRates)}: {DepositAccountInterestRates}";
    }
}