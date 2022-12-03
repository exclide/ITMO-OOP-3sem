using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class ClientBuilder
{
    private ClientName _clientName;
    private ClientAddress _clientAddress;
    private ClientPassportId _clientPassportId;

    public ClientBuilder SetClientName(ClientName clientName)
    {
        ArgumentNullException.ThrowIfNull(clientName);
        _clientName = clientName;
        return this;
    }

    public ClientBuilder SetClientAddress(ClientAddress clientAddress)
    {
        ArgumentNullException.ThrowIfNull(clientAddress);
        _clientAddress = clientAddress;
        return this;
    }

    public ClientBuilder SetClientPassportId(ClientPassportId clientPassportId)
    {
        ArgumentNullException.ThrowIfNull(clientPassportId);
        _clientPassportId = clientPassportId;
        return this;
    }

    public Client GetClient(int clientId)
    {
        if (_clientName is null)
        {
            throw new BankException("Client name was null");
        }

        return new Client(_clientName, _clientAddress, _clientPassportId, clientId);
    }
}