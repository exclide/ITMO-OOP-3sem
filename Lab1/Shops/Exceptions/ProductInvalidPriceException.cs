namespace Shops.Exceptions;

public class ProductInvalidPriceException : Exception
{
    public ProductInvalidPriceException(string message)
        : base(message)
    {
    }
}