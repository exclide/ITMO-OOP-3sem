namespace Shops.Exceptions;

public class ProductInvalidQuantityException : Exception
{
    public ProductInvalidQuantityException(string message)
        : base(message)
    {
    }
}