using Banks.Accounts;
using Banks.Entities;
using Banks.Models;
using Banks.Services;

namespace Banks.Console.ConsoleInterface;
using System;

public class ConsoleCommands
{
    private readonly CentralBankService _cb;
    public ConsoleCommands(CentralBankService cb)
    {
        _cb = cb;
        MainMenu();
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Manage banks\n" +
                          "2. Manage clients\n" +
                          "3. Manage accounts\n" +
                          "4. Manage time\n" +
                          "Type number to continue...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                ManageBanks();
                break;
            case "2":
                ManageClients();
                break;
            case "3":
                ManageAccounts();
                break;
            case "4":
                ManageTime();
                break;
            default:
                MainMenu();
                break;
        }
    }

    public void ManageBanks()
    {
        Console.Clear();
        Console.WriteLine("1. Create bank\n" +
                          "2. Edit banks\n" +
                          "3. List banks\n" +
                          "4. Back to menu\n" +
                          "Type number to continue...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Enter bank name");
                string bankName = Console.ReadLine();
                _cb.RegisterNewBank(bankName, GetBankConfig());
                ManageBanks();
                break;
            case "2":
            {
                Console.Clear();
                var banks = _cb.Banks;
                foreach (var bank in banks)
                {
                    Console.WriteLine($"{bank.Id}. {bank.BankName},  Config: {bank.BankConfig}");
                }

                Console.WriteLine("\nEnter bank number to edit Bank Config\n");
                int num = Convert.ToInt32(Console.ReadLine());
                var bankToEdit = banks.First(x => x.Id == num);
                var newBankCfg = GetBankConfig();
                bankToEdit.BankConfig = newBankCfg;
                ManageBanks();
                break;
            }

            case "3":
            {
                Console.Clear();
                var banks = _cb.Banks;
                foreach (var bank in banks)
                {
                    Console.WriteLine($"{bank.Id}. {bank.BankName}, Clients: {bank.Clients.Count()}, Accounts: {bank.Accounts.Count()} Config: {bank.BankConfig}");
                }

                Console.WriteLine("\nPress any key to go back\n");
                Console.ReadLine();
                ManageBanks();

                break;
            }

            case "4":
                MainMenu();
                break;
            default:
                ManageBanks();
                break;
        }
    }

    public BankConfig GetBankConfig()
    {
        Console.Clear();
        var bankCfgBuilder = new BankConfigBuilder();
        Console.WriteLine("Enter Unverified Transaction Limit");
        decimal val = Convert.ToDecimal(Console.ReadLine());
        bankCfgBuilder.SetUnverifiedTransactionLimit(val);
        Console.WriteLine("Enter Credit Account Comission");
        val = Convert.ToDecimal(Console.ReadLine());
        bankCfgBuilder.SetCreditAccountComission(val);
        Console.WriteLine("Enter Debit Account Rate");
        val = Convert.ToDecimal(Console.ReadLine());
        bankCfgBuilder.SetDebitAccountRate(val);
        Console.WriteLine("Enter Deposit Account Ranges (two values, from lowest to highest)");
        string depositRanges = Console.ReadLine();
        var data = depositRanges.Split(' ').Select(decimal.Parse).ToList();
        bankCfgBuilder.SetDepositAccountRanges(data[0], data[1]);
        Console.WriteLine("Enter Deposit Account Rates (three values, from lowest to highest)");
        string depositRates = Console.ReadLine();
        var data2 = depositRates.Split(' ').Select(decimal.Parse).ToList();
        bankCfgBuilder.SetDepositAccountRates(data2[0], data2[1], data2[2]);

        return bankCfgBuilder.GetBankConfig();
    }

    public void ManageClients()
    {
        Console.Clear();
        Console.WriteLine("1. Register client\n" +
                          "2. List clients\n" +
                          "3. Edit clients\n" +
                          "4. Back to menu\n" +
                          "Type number to continue...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
            {
                foreach (var bank in _cb.Banks)
                {
                    Console.WriteLine($"{bank.Id}. {bank.BankName},  Config: {bank.BankConfig}");
                }

                Console.WriteLine("Choose bank ID to register in");
                var bankId = Convert.ToInt32(Console.ReadLine());
                var choosenBank = _cb.Banks.First(x => x.Id == bankId);
                Console.Clear();
                var clientBuilder = GetClientBuilder();
                var client = _cb.RegisterNewClient(choosenBank, clientBuilder);
                ManageClients();
                break;
            }

            case "2":
            {
                Console.Clear();
                foreach (var client in _cb.Clients)
                {
                    Console.WriteLine($"{client.Id}. {client.ClientName} {client.ClientAddress ?? null}");
                }

                Console.WriteLine("\nPress any key to go back\n");
                Console.ReadLine();
                ManageClients();
                break;
            }

            case "3":
            {
                Console.Clear();
                foreach (var client in _cb.Clients)
                {
                    Console.WriteLine($"{client.Id}. {client.ClientName} {client.ClientAddress ?? null}");
                }

                Console.WriteLine("\nType client ID to change name/adr/passport\n");
                int clientId = Convert.ToInt32(Console.ReadLine());
                var choosenClient = _cb.Clients.First(x => x.Id == clientId);

                Console.Clear();
                Console.WriteLine("1. Change name\n" +
                                  "2. Change adr\n" +
                                  "3. Change passport\n" +
                                  "4. Back\n" +
                                  "Type number to continue...");
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter new first name");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter new second name");
                        string secondName = Console.ReadLine();
                        choosenClient.ClientName = new ClientName(firstName, secondName);
                        ManageClients();
                        break;
                    case "2":
                        Console.Clear();
                        var clientAdrBuilder = new ClientAddressBuilder();
                        Console.WriteLine("Enter city");
                        string city = Console.ReadLine();
                        clientAdrBuilder.SetCity(city);
                        Console.WriteLine("Enter street name");
                        string streetName = Console.ReadLine();
                        clientAdrBuilder.SetStreetName(streetName);
                        Console.WriteLine("Enter postal code");
                        string postalCode = Console.ReadLine();
                        clientAdrBuilder.SetPostalCode(Convert.ToInt32(postalCode));
                        Console.WriteLine("Enter house number");
                        string houseNumber = Console.ReadLine();
                        clientAdrBuilder.SetHouseNumber(Convert.ToInt32(houseNumber));
                        Console.WriteLine("Enter apartment number");
                        string apartmentNumber = Console.ReadLine();
                        clientAdrBuilder.SetApartmentNumber(Convert.ToInt32(apartmentNumber));
                        choosenClient.ClientAddress = clientAdrBuilder.GetClientAddress();
                        ManageClients();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter new passport ID");
                        string passportId = Console.ReadLine();
                        choosenClient.ClientPassportId = new ClientPassportId(passportId);
                        ManageClients();
                        break;
                    case "4":
                        ManageClients();
                        break;
                    default:
                        ManageClients();
                        break;
                }

                break;
            }

            case "4":
                MainMenu();
                break;
            default:
                ManageClients();
                break;
        }
    }

    public ClientBuilder GetClientBuilder()
    {
        Console.WriteLine("Enter client's first name");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter client's second name");
        string secondName = Console.ReadLine();
        var clientBuilder = new ClientBuilder();
        clientBuilder.SetClientName(new ClientName(firstName, secondName));
        Console.WriteLine("Would you like to verify your registration? [y/n]");
        string ans = Console.ReadLine();
        if (ans.Equals("n"))
        {
            return clientBuilder;
        }

        var clientAdrBuilder = new ClientAddressBuilder();
        Console.WriteLine("Enter passport ID\n");
        string passportId = Console.ReadLine();
        clientBuilder.SetClientPassportId(new ClientPassportId(passportId));
        Console.WriteLine("Enter city\n");
        string city = Console.ReadLine();
        clientAdrBuilder.SetCity(city);
        Console.WriteLine("Enter street name\n");
        string streetName = Console.ReadLine();
        clientAdrBuilder.SetStreetName(streetName);
        Console.WriteLine("Enter postal code\n");
        string postalCode = Console.ReadLine();
        clientAdrBuilder.SetPostalCode(Convert.ToInt32(postalCode));
        Console.WriteLine("Enter house number\n");
        string houseNumber = Console.ReadLine();
        clientAdrBuilder.SetHouseNumber(Convert.ToInt32(houseNumber));
        Console.WriteLine("Enter apartment number\n");
        string apartmentNumber = Console.ReadLine();
        clientAdrBuilder.SetApartmentNumber(Convert.ToInt32(apartmentNumber));
        clientBuilder.SetClientAddress(clientAdrBuilder.GetClientAddress());
        return clientBuilder;
    }

    public void ManageAccounts()
    {
        Console.Clear();
        Console.WriteLine("1. Register account\n" +
                          "2. List accounts\n" +
                          "3. Edit accounts\n" +
                          "4. Back to menu\n" +
                          "Type number to continue...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
            {
                Console.Clear();
                foreach (var bank in _cb.Banks)
                {
                    Console.WriteLine($"{bank.Id}. {bank.BankName},  Config: {bank.BankConfig}");
                }

                Console.WriteLine("Choose bank ID to register in\n");
                var bankId = Convert.ToInt32(Console.ReadLine());
                var choosenBank = _cb.Banks.First(x => x.Id == bankId);
                Console.Clear();

                foreach (var client in choosenBank.Clients)
                {
                    Console.WriteLine($"{client.Id}. {client.ClientName} {client.ClientAddress ?? null}");
                }

                Console.WriteLine("Choose client ID to register on\n");
                int clientId = Convert.ToInt32(Console.ReadLine());
                var choosenClient = _cb.Clients.First(x => x.Id == clientId);

                Console.Clear();
                Console.WriteLine("1. Debit\n" +
                                  "2. Deposit\n" +
                                  "3. Credit\n" +
                                  "Choose account type...");
                var accountType = (AccountType)(Convert.ToInt32(Console.ReadLine()) - 1);

                Console.Clear();
                Console.WriteLine("Type in deposit amount\n");
                decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                _cb.RegisterNewAccount(choosenBank, choosenClient, accountType, depositAmount);
                ManageAccounts();
                break;
            }

            case "2":
            {
                Console.Clear();
                foreach (var acc in _cb.Accounts)
                {
                    Console.WriteLine(acc);
                }

                Console.WriteLine("\nPress any key to go back\n");
                Console.ReadLine();
                ManageAccounts();

                break;
            }

            case "3":
            {
                Console.Clear();
                foreach (var acc in _cb.Accounts)
                {
                    Console.WriteLine(acc);
                }

                Console.WriteLine("\nChoose account ID to edit\n");
                int accountId = Convert.ToInt32(Console.ReadLine());
                var account = _cb.Accounts.First(x => x.AccountId == accountId);
                Console.Clear();

                foreach (var trans in account.TransactionHistory)
                {
                    Console.WriteLine(trans);
                }

                Console.WriteLine("Type transaction ID to cancel\n" +
                                  "Or type a letter to go back\n");
                string inp = Console.ReadLine();
                int transId = 0;
                bool isInputNumeric = int.TryParse(inp, out transId);

                if (isInputNumeric)
                {
                    account.RevertTransaction(transId);
                }

                ManageAccounts();

                break;
            }

            case "4":
            {
                MainMenu();
                break;
            }

            default:
                ManageAccounts();
                break;
        }
    }

    public void ManageTime()
    {
        Console.Clear();
        Console.WriteLine("1. Get time\n" +
                          "2. Add days\n" +
                          "3. Add months\n" +
                          "4. Add years\n" +
                          "5. Back to menu\n" +
                          "Type number to continue...");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine($"Current time is {_cb.TimeMachine.Date}\n" +
                                  $"Press any key to go back...");
                Console.ReadLine();
                ManageTime();
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Type the number of days to add\n");
                int days = Convert.ToInt32(Console.ReadLine());
                _cb.TimeMachine.AddDays(days);
                ManageTime();
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Type the number of months to add\n");
                int months = Convert.ToInt32(Console.ReadLine());
                _cb.TimeMachine.AddMonths(months);
                ManageTime();
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("Type the number of years to add\n");
                int years = Convert.ToInt32(Console.ReadLine());
                _cb.TimeMachine.AddYears(years);
                ManageTime();
                break;
            case "5":
                MainMenu();
                break;
            default:
                ManageTime();
                break;
        }
    }
}