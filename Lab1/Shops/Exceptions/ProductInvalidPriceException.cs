namespace Shops.Exceptions;

public class ProductInvalidPrice : Exception
{
    public ProductInvalidPrice(string message)
        : base(message)
    {
    }
}