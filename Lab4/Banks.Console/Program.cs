using Banks.Accounts;
using Banks.Entities;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;
public class Program
{
    public static void Main(string[] args)
    {
        var cb = new CentralBankService();
        var bank = cb.RegisterNewBank(
            "Sber",
            new BankConfig(
                100,
                100,
                5,
                new DepositAccountInterestRates(50, 100, 2, 3, 4)));
        var client = cb.RegisterNewClient(bank, new ClientName("Lol", "Kek"));
        cb.SubscribeClientToBankLimitChanges(bank, client);
        bank.BankConfig = new BankConfig(
            40,
            50,
            1,
            new DepositAccountInterestRates(40, 50, 1, 2, 3));

        var acc = new DebitAccount(client, bank, 0, 5000);

        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        date = date.AddYears(5);

        System.Console.WriteLine(date);
    }
}