using Banks.Models;

namespace Banks.Entities;

public class Client : IEquatable<Client>
{
    private readonly int _id;
    public Client(ClientName clientName, ClientAddress clientAddress, ClientPassportId clientPassportId, int id)
    {
        ArgumentNullException.ThrowIfNull(clientName);
        ArgumentNullException.ThrowIfNull(clientAddress);
        ArgumentNullException.ThrowIfNull(clientPassportId);

        ClientName = clientName;
        ClientAddress = clientAddress;
        ClientPassportId = clientPassportId;
        _id = id;
    }

    public ClientName ClientName { get; set; }
    public ClientAddress ClientAddress { get; set; }
    public ClientPassportId ClientPassportId { get; set; }

    public bool IsVerified => ClientAddress is not null && ClientPassportId is not null;

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Client);
    public bool Equals(Client other) => other?._id.Equals(_id) ?? false;
}