namespace Shops.Exceptions;

public class ProductNotEnoughQuantityException : Exception
{
    public ProductNotEnoughQuantityException(string message)
        : base(message)
    {
    }
}