using Shops.Exceptions;

namespace Shops.Entities;

public class Client
{
    private readonly int _id;
    private string _name;

    public Client(int id, string name, decimal cash)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(name)} was null or whitespace.");
        }

        _id = id;
        _name = name;
        Cash = cash;
    }

    public decimal Cash { get; set; }
}