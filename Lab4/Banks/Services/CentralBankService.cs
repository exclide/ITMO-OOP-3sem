using Banks.Accounts;
using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Services;

public class CentralBankService
{
    private readonly ICollection<Bank> _banks;
    private readonly ICollection<Client> _clients;
    private readonly ICollection<IAccount> _accounts;
    private readonly TimeMachine _timeMachine;
    private readonly DateOnly _startDate = new DateOnly(2023, 1, 1);

    public CentralBankService()
    {
        _banks = new List<Bank>();
        _clients = new List<Client>();
        _accounts = new List<IAccount>();
        _timeMachine = new TimeMachine(_startDate);
    }

    public TimeMachine TimeMachine => _timeMachine;
    public IEnumerable<Bank> Banks => _banks;
    public IEnumerable<Client> Clients => _clients;
    public IEnumerable<IAccount> Accounts => _accounts;

    public Bank RegisterNewBank(string bankName, BankConfig bankConfig)
    {
        Bank bank = new Bank(bankName, bankConfig, _banks.Count);
        _banks.Add(bank);
        return bank;
    }

    public Client RegisterNewClient(Bank bank, ClientName clientName, ClientAddress clientAddress = null, ClientPassportId clientPassportId = null)
    {
        var targetBank = _banks.FirstOrDefault(b => b.Equals(bank));
        if (targetBank is null)
        {
            throw new BankException("Bank isn't registered in the central bank");
        }

        ClientBuilder clientBuilder = new ClientBuilder()
            .SetClientName(clientName);

        if (clientAddress is not null)
        {
            clientBuilder = clientBuilder.SetClientAddress(clientAddress);
        }

        if (clientPassportId is not null)
        {
            clientBuilder = clientBuilder.SetClientPassportId(clientPassportId);
        }

        Client client = clientBuilder.GetClient(_clients.Count);

        bank.AddClient(client);
        _clients.Add(client);
        return client;
    }

    public Client RegisterNewClient(Bank bank, ClientBuilder clientBuilder)
    {
        var targetBank = _banks.FirstOrDefault(b => b.Equals(bank));
        if (targetBank is null)
        {
            throw new BankException("Bank isn't registered in the central bank");
        }

        Client client = clientBuilder.GetClient(_clients.Count);

        bank.AddClient(client);
        _clients.Add(client);
        return client;
    }

    public IAccount RegisterNewAccount(Bank bank, Client client, AccountType accountType, decimal depositAmount = 0)
    {
        var targetBank = _banks.FirstOrDefault(b => b.Equals(bank));
        if (targetBank is null)
        {
            throw new BankException("Bank isn't registered in the central bank");
        }

        var targetClient = _clients.FirstOrDefault(c => c.Equals(client));
        if (targetClient is null)
        {
            throw new BankException("Client isn't registered in the central bank");
        }

        IAccount account = accountType switch
        {
            AccountType.Credit => new CreditAccount(client, bank, _accounts.Count, _timeMachine.Date, depositAmount),
            AccountType.Debit => new DebitAccount(client, bank, _accounts.Count, _timeMachine.Date, depositAmount),
            AccountType.Deposit => new DepositAccount(client, bank, _accounts.Count, _timeMachine.Date, depositAmount),
            _ => throw new BankException("Account type not implemented"),
        };

        bank.AddAccount(account, client);
        _accounts.Add(account);

        _timeMachine.Subscribe(account);
        return account;
    }

    public void TransferFromAccountTo(int accountIdFrom, int accountIdTo, decimal transferAmount)
    {
        var accountFrom = _accounts.FirstOrDefault(x => x.AccountId.Equals(accountIdFrom));
        var accountTo = _accounts.FirstOrDefault(x => x.AccountId.Equals(accountIdTo));

        if (accountFrom is null || accountTo is null)
        {
            throw new BankException("Can't find the required accounts");
        }

        var transferTransaction = new TransferTransaction(accountFrom, accountTo, transferAmount);
        accountFrom.MakeTransaction(transferTransaction);
    }

    public void SubscribeClientToBankLimitChanges(Bank bank, Client client)
    {
        if (!(_banks.Contains(bank) || _clients.Contains(client)))
        {
            throw new BankException("Bank or client isn't registered");
        }

        client.Subscribe(bank);
    }

    public void UnsubscribeClientFromBankLimitChanges(Client client)
    {
        if (!_clients.Contains(client))
        {
            throw new BankException("Client isn't registered");
        }

        client.Unsubscribe();
    }
}