using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Entities;

public class Bank : IEquatable<Bank>, IObservable<BankConfig>
{
    private readonly int _id;

    private readonly ICollection<Client> _clients;
    private readonly ICollection<IAccount> _accounts;
    private readonly ICollection<IObserver<BankConfig>> _observers;
    private BankConfig _bankConfig;
    public Bank(string bankName, BankConfig bankConfig, int id)
    {
        ArgumentNullException.ThrowIfNull(bankConfig);
        if (string.IsNullOrEmpty(bankName))
        {
            throw new BankException("bank name was null or empty");
        }

        BankName = bankName;
        _bankConfig = bankConfig;
        _id = id;
        _clients = new List<Client>();
        _accounts = new List<IAccount>();
        _observers = new List<IObserver<BankConfig>>();
    }

    public string BankName { get; }
    public int Id => _id;

    public BankConfig BankConfig
    {
        get => _bankConfig;
        set
        {
            _bankConfig = value;
            foreach (var observer in _observers)
            {
                observer.OnNext(_bankConfig);
            }
        }
    }

    public IEnumerable<Client> Clients => _clients;
    public IEnumerable<IAccount> Accounts => _accounts;

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Bank);
    public bool Equals(Bank other) => other?._id.Equals(_id) ?? false;

    public override string ToString()
    {
        return $"{nameof(_id)}: {_id}, {nameof(BankName)}: {BankName}, {nameof(BankConfig)}: {BankConfig}";
    }

    public void AddClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        _clients.Add(client);
    }

    public void AddAccount(IAccount account, Client client)
    {
        ArgumentNullException.ThrowIfNull(account);
        var targetClient = _clients.FirstOrDefault(c => c.Equals(client));
        if (targetClient is null)
        {
            throw new BankException("Client wasn't found in the bank");
        }

        _accounts.Add(account);
        client.AddAccount(account);
    }

    public IDisposable Subscribe(IObserver<BankConfig> observer)
    {
        if (_observers.Contains(observer))
        {
            throw new BankException("Client already subscribed");
        }

        _observers.Add(observer);

        return new Unsubscriber(_observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private ICollection<IObserver<BankConfig>> _observers;
        private IObserver<BankConfig> _observer;

        public Unsubscriber(ICollection<IObserver<BankConfig>> observers, IObserver<BankConfig> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer is not null)
            {
                _observers.Remove(_observer);
            }
        }
    }
}