namespace Shops.Entities;

public class Shop
{
    private int _id;
    private string _name;
    private string _address;

    public Shop(int id, string name, string address)
    {
        _id = id;
        _name = name;
        _address = address;
    }
}