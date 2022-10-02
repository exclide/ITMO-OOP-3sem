namespace Shops.Exceptions;

public class ProductInvalidQuantity : Exception
{
    public ProductInvalidQuantity(string message)
        : base(message)
    {
    }
}