using Banks.Entities;
using Banks.Models;

namespace Banks.Services;

public class CentralBankService
{
    private readonly ICollection<Bank> _banks;
    private readonly ICollection<Client> _clients;

    public CentralBankService()
    {
        _banks = new List<Bank>();
        _clients = new List<Client>();
    }

    public Bank RegisterNewBank(string bankName, BankConfig bankConfig)
    {
        Bank bank = new Bank(bankName, bankConfig, _banks.Count);
        _banks.Add(bank);
        return bank;
    }

    public Client RegisterNewClient(ClientName clientName, ClientAddress clientAddress = null, ClientPassportId clientPassportId = null)
    {
        Client client = new ClientBuilder()
            .SetClientName(clientName)
            .SetClientAddress(clientAddress)
            .SetClientPassportId(clientPassportId)
            .SetClientId(_clients.Count)
            .GetClient();

        _clients.Add(client);
        return client;
    }

    public void AddClientToBank(Bank bank, Client client)
    {
    }
}