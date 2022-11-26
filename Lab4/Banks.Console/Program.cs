using Banks.Accounts;
using Banks.Console.ConsoleInterface;
using Banks.Entities;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;
public class Program
{
    public static void Main(string[] args)
    {
        var centralBank = new CentralBankService();

        BankConfig sberConfig = new BankConfigBuilder()
            .SetCreditAccountComission(100)
            .SetUnverifiedTransactionLimit(5)
            .SetDebitAccountRate(5)
            .SetDepositAccountRanges(500, 1000)
            .SetDepositAccountRates(3, 4, 5)
            .GetBankConfig();
        var sberBank = centralBank.RegisterNewBank("Sber", sberConfig);

        BankConfig tinkConfig = new BankConfigBuilder()
            .SetCreditAccountComission(200)
            .SetUnverifiedTransactionLimit(10)
            .SetDebitAccountRate(10)
            .SetDepositAccountRanges(5000, 10000)
            .SetDepositAccountRates(6, 8, 10)
            .GetBankConfig();
        var tinkBank = centralBank.RegisterNewBank("Tink", tinkConfig);

        BankConfig alphaConfig = new BankConfigBuilder()
            .SetCreditAccountComission(1000)
            .SetUnverifiedTransactionLimit(5000)
            .SetDebitAccountRate(50)
            .SetDepositAccountRanges(500, 1000)
            .SetDepositAccountRates(30, 40, 50)
            .GetBankConfig();
        var alphaBank = centralBank.RegisterNewBank("Alpha", alphaConfig);

        Client client1 = centralBank.RegisterNewClient(alphaBank, new ClientName("Ivan", "Govnov"));

        var cc = new ConsoleCommands(centralBank);
    }
}