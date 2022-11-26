using Banks.Accounts;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;
using Banks.Services;
using Xunit;
using Xunit.Abstractions;

namespace Banks.Test;

public class BankTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly CentralBankService _cb;
    private readonly Bank _sber;
    private readonly Bank _tink;
    private readonly Bank _alpha;

    public BankTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _cb = new CentralBankService();

        BankConfig sberConfig = new BankConfigBuilder()
            .SetCreditAccountComission(100)
            .SetUnverifiedTransactionLimit(5)
            .SetDebitAccountRate(365M)
            .SetDepositAccountRanges(500, 1000)
            .SetDepositAccountRates(3, 4, 5)
            .GetBankConfig();

        _sber = _cb.RegisterNewBank("Sber", sberConfig);

        BankConfig tinkConfig = new BankConfigBuilder()
            .SetCreditAccountComission(200)
            .SetUnverifiedTransactionLimit(10)
            .SetDebitAccountRate(10)
            .SetDepositAccountRanges(5000, 10000)
            .SetDepositAccountRates(6, 8, 10)
            .GetBankConfig();

        _tink = _cb.RegisterNewBank("Tink", tinkConfig);

        BankConfig alphaConfig = new BankConfigBuilder()
            .SetCreditAccountComission(1000)
            .SetUnverifiedTransactionLimit(5000)
            .SetDebitAccountRate(5)
            .SetDepositAccountRanges(50, 100)
            .SetDepositAccountRates(365, 730, 1095)
            .GetBankConfig();

        _alpha = _cb.RegisterNewBank("Alpha", alphaConfig);
    }

    [Fact]
    public void CreateDebitAccount_CheckRates()
    {
        var client = _cb.RegisterNewClient(_sber, new ClientName("Ivan", "Govnov"));
        var account = _cb.RegisterNewAccount(_sber, client, "debit", 100M);
        _cb.TimeMachine.AddMonths(1);
        Assert.Equal(131M, account.Balance);
        _cb.TimeMachine.AddMonths(1);
        Assert.Equal(167.68M, account.Balance);
    }

    [Fact]
    public void CreateDepositAccount_Transfer_ChangeBankConfig_CheckRates()
    {
        var client = _cb.RegisterNewClient(_alpha, new ClientName("Ivan", "Govnov"));
        var account = _cb.RegisterNewAccount(_alpha, client, "deposit", 100M);
        _cb.TimeMachine.AddMonths(1);
        Assert.Equal(193M, account.Balance);
        var client2 = _cb.RegisterNewClient(_alpha, new ClientName("Oleg", "Govnov"));
        var account2 = _cb.RegisterNewAccount(_alpha, client, "debit", 100M);
        _cb.TransferFromAccountTo(account2.AccountId, account.AccountId, 7M);
        Assert.Equal(200M, account.Balance);
        Assert.Equal(93M, account2.Balance);

        _alpha.BankConfig = new BankConfigBuilder()
            .SetCreditAccountComission(20)
            .SetDebitAccountRate(730)
            .SetUnverifiedTransactionLimit(20)
            .SetDepositAccountRanges(1, 2)
            .SetDepositAccountRates(1, 2, 3650)
            .GetBankConfig();

        _cb.TimeMachine.AddMonths(1);
        Assert.Equal(760M, account.Balance);
        Assert.Equal(145.08M, account2.Balance);
    }

    [Fact]
    public void CreateAccountController_TestAccountLimits()
    {
        var client = _cb.RegisterNewClient(_alpha, new ClientName("Ivan", "Govnov"));
        var account = _cb.RegisterNewAccount(_alpha, client, "deposit", 100M);
        var accountController = new ClientAccountController(account);
        Assert.Throws<BankException>(() => accountController.WithdrawFromAccount(50));
        _cb.TimeMachine.AddYears(3);
        accountController.WithdrawFromAccount(50);
    }
}