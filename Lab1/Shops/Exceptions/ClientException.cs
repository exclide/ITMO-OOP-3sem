namespace Shops.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message)
    {
    }

    public static ClientException InvalidCash(decimal cashEntered)
    {
        return new ClientException($"Cash was: {cashEntered}");
    }

    public static ClientException NotEnoughMoney(decimal clientMoney, decimal neededMoney)
    {
        return new ClientException($"Client has: {clientMoney}$ but needs: {neededMoney}$");
    }
}