using Banks.Exceptions;

namespace Banks.Entities;

public class Bank : IEquatable<Bank>
{
    private readonly int _id;
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
    }

    public string BankName { get; }
    public BankConfig BankConfig { get; }

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Bank);
    public bool Equals(Bank other) => other?._id.Equals(_id) ?? false;
}