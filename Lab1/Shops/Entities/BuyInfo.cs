using Shops.Exceptions;

namespace Shops.Entities;

public class BuyInfo : IEquatable<BuyInfo>
{
    public BuyInfo(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (quantity < 0)
        {
            throw new ProductInvalidQuantityException($"Product quantity was negative: {quantity}");
        }

        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; set; }

    public override int GetHashCode() => Product.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Product);
    public bool Equals(BuyInfo other) => other?.Product.Equals(Product) ?? false;
}