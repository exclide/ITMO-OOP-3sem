namespace Shops.Exceptions;

public class ClientNotEnoughMoneyException : Exception
{
    public ClientNotEnoughMoneyException(string message)
        : base(message)
    {
    }
}