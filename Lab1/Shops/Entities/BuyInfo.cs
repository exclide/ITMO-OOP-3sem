using Shops.Exceptions;

namespace Shops.Entities;

public class BuyInfo : IEquatable<BuyInfo>
{
    private int _quantity;
    public BuyInfo(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (quantity < 0)
        {
            throw ProductException.InvalidQuantity(quantity);
        }

        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
            {
                throw ProductException.InvalidQuantity(value);
            }

            _quantity = value;
        }
    }

    public override int GetHashCode() => Product.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Product);
    public bool Equals(BuyInfo other) => other?.Product.Equals(Product) ?? false;
}