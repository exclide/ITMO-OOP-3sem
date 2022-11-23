using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Entities;

public class Bank : IEquatable<Bank>
{
    private readonly int _id;
    private readonly ICollection<Client> _clients;
    private readonly ICollection<IAccount> _accounts;
    public Bank(string bankName, BankConfig bankConfig, int id)
    {
        ArgumentNullException.ThrowIfNull(bankConfig);
        if (string.IsNullOrEmpty(bankName))
        {
            throw new BankException("bank name was null or empty");
        }

        BankName = bankName;
        BankConfig = bankConfig;
        _id = id;
        _clients = new List<Client>();
        _accounts = new List<IAccount>();
    }

    public string BankName { get; }
    public BankConfig BankConfig { get; }
    public IEnumerable<Client> Clients => _clients;
    public IEnumerable<IAccount> Accounts => _accounts;

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Bank);
    public bool Equals(Bank other) => other?._id.Equals(_id) ?? false;

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
}