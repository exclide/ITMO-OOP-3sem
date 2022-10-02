using Shops.Exceptions;

namespace Shops.Entities;

public class ProductInfo : IEquatable<ProductInfo>
{
    public ProductInfo(Product product, int quantity, decimal price)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (quantity < 0)
        {
            throw new ProductInvalidQuantityException($"Product quantity was negative: {quantity}");
        }

        if (price < 0)
        {
            throw new ProductInvalidPriceException($"Product price was negative: {price}");
        }

        Product = product;
        Quantity = quantity;
        Price = price;
    }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; }

    public override int GetHashCode() => Product.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as ProductInfo);
    public bool Equals(ProductInfo other) => other?.Product.Equals(Product) ?? false;
}