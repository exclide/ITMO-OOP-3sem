using Banks.Accounts;
using Banks.Models;

namespace Banks.Entities;

public class Client : IEquatable<Client>, IObserver<BankConfig>
{
    private readonly int _id;
    private readonly ICollection<IAccount> _accounts;
    private IDisposable _unsubscriber;
    private ClientName _clientName;
    private ClientAddress _clientAddress;
    private ClientPassportId _clientPassportId;

    public Client(ClientName clientName, ClientAddress clientAddress, ClientPassportId clientPassportId, int id)
    {
        ArgumentNullException.ThrowIfNull(clientName);

        _clientName = clientName;
        _clientAddress = clientAddress;
        _clientPassportId = clientPassportId;
        _id = id;
        _accounts = new List<IAccount>();
    }

    public ClientName ClientName
    {
        get => _clientName;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _clientName = value;
        }
    }

    public ClientAddress ClientAddress
    {
        get => _clientAddress;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _clientAddress = value;
        }
    }

    public ClientPassportId ClientPassportId
    {
        get => _clientPassportId;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _clientPassportId = value;
        }
    }

    public int Id => _id;
    public IEnumerable<IAccount> Accounts => _accounts;

    public bool IsVerified => ClientAddress is not null && ClientPassportId is not null;

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Client);
    public bool Equals(Client other) => other?._id.Equals(_id) ?? false;

    public void AddAccount(IAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _accounts.Add(account);
    }

    public void Subscribe(IObservable<BankConfig> provider)
    {
        _unsubscriber = provider.Subscribe(this);
    }

    public void Unsubscribe()
    {
        _unsubscriber.Dispose();
    }

    public void OnCompleted()
    {
        Console.WriteLine("Bank no longer offers limit subscription");
        Unsubscribe();
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(BankConfig value)
    {
        Console.WriteLine($"Bank has changed its limits: {value}");
    }
}