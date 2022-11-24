using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class BankConfigBuilder
{
    private decimal _unverifiedTransactionLimit;
    private decimal _creditAccountComission;
    private decimal _debitAccountRate;
    private decimal _depositFirstRange;
    private decimal _depositSecondRange;
    private decimal _depositAccountFirstRate;
    private decimal _depositAccountSecondRate;
    private decimal _depositAccountThirdRate;
    private int bitSet;

    public BankConfigBuilder SetUnverifiedTransactionLimit(decimal limit)
    {
        if (limit < 0)
        {
            throw new BankException("Limit can't be negative");
        }

        _unverifiedTransactionLimit = limit;
        bitSet |= 1 << 0;
        return this;
    }

    public BankConfigBuilder SetCreditAccountComission(decimal comission)
    {
        if (comission < 0)
        {
            throw new BankException("Comission can't be negative");
        }

        _creditAccountComission = comission;
        bitSet |= 1 << 1;
        return this;
    }

    public BankConfigBuilder SetDebitAccountRate(decimal rate)
    {
        if (rate < 0)
        {
            throw new BankException("Rate can't be negative");
        }

        _debitAccountRate = rate;
        bitSet |= 1 << 2;
        return this;
    }

    public BankConfigBuilder SetDepositAccountRanges(decimal firstRange, decimal secondRange)
    {
        _depositFirstRange = firstRange;
        _depositSecondRange = secondRange;
        bitSet |= 1 << 3;
        return this;
    }

    public BankConfigBuilder SetDepositAccountRates(decimal firstRate, decimal secondRate, decimal thirdRate)
    {
        _depositAccountFirstRate = firstRate;
        _depositAccountSecondRate = secondRate;
        _depositAccountThirdRate = thirdRate;
        bitSet |= 1 << 4;
        return this;
    }

    public BankConfig GetBankConfig()
    {
        if (bitSet != 31)
        {
            throw new BankException("Some fields weren't set");
        }

        return new BankConfig(
            _unverifiedTransactionLimit,
            _creditAccountComission,
            _debitAccountRate,
            new DepositAccountInterestRates(
                _depositFirstRange,
                _depositSecondRange,
                _depositAccountFirstRate,
                _depositAccountSecondRate,
                _depositAccountThirdRate));
    }
}