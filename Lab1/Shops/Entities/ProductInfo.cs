using Shops.Exceptions;

namespace Shops.Entities;

public class ProductInfo : IEquatable<ProductInfo>
{
    private int _quantity;
    private decimal _price;
    public ProductInfo(Product product, int quantity, decimal price)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (quantity < 0)
        {
            throw ProductException.InvalidQuantity(quantity);
        }

        if (price < 0)
        {
            throw ProductException.InvalidPrice(price);
        }

        Product = product;
        Quantity = quantity;
        Price = price;
    }

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

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw ProductException.InvalidPrice(value);
            }

            _price = value;
        }
    }

    public Product Product { get; }

    public override int GetHashCode() => Product.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as ProductInfo);
    public bool Equals(ProductInfo other) => other?.Product.Equals(Product) ?? false;
}