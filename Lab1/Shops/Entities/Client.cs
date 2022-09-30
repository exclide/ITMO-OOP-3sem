namespace Shops.Entities;

public class Client
{
    private string _name;
    private int _cash;

    public Client(string name, int cash)
    {
        _name = name;
        _cash = cash;
    }
}