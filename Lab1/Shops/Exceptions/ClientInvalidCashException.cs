namespace Shops.Exceptions;

public class ClientInvalidCashException : Exception
{
    public ClientInvalidCashException(string message)
        : base(message)
    {
    }
}