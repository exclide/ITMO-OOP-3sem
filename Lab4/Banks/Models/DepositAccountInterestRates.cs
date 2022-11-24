using Banks.Exceptions;

namespace Banks.Models;

public class DepositAccountInterestRates
{
    public DepositAccountInterestRates(
        decimal firstRange,
        decimal secondRange,
        decimal firstPercent,
        decimal secondPercent,
        decimal thirdPercent)
    {
        if (firstRange < 0 || secondRange < 0 || firstRange >= secondRange)
        {
            throw new BankException("Invalid deposit account deposit ranges");
        }

        if (firstPercent < 0 || secondPercent < 0 || thirdPercent < 0
            || firstPercent > secondPercent || secondPercent > thirdPercent)
        {
            throw new BankException("Invalid deposit account interest rates");
        }

        FirstRange = firstRange;
        SecondRange = secondRange;
        FirstPercent = firstPercent;
        SecondPercent = secondPercent;
        ThirdPercent = thirdPercent;
    }

    public decimal FirstRange { get; }
    public decimal SecondRange { get; }
    public decimal FirstPercent { get; }
    public decimal SecondPercent { get; }
    public decimal ThirdPercent { get; }

    public override string ToString()
    {
        return $"{nameof(FirstRange)}: {FirstRange}, {nameof(SecondRange)}: {SecondRange}, " +
               $"{nameof(FirstPercent)}: {FirstPercent}, {nameof(SecondPercent)}: {SecondPercent}, " +
               $"{nameof(ThirdPercent)}: {ThirdPercent}";
    }
}