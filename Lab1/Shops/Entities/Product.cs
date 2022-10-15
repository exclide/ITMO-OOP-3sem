using Shops.Exceptions;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    private readonly int _id;

    public Product(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(name)} was null or whitespace.");
        }

        _id = id;
        Name = name;
    }

    public string Name { get; }
    public int Id => _id;

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Product);
    public bool Equals(Product other) => other?._id.Equals(_id) ?? false;
}