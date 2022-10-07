using Shops.Exceptions;

namespace Shops.Entities;

public class BuyInfo
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
}