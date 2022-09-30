namespace Shops.Entities;

public class Product
{
    private int _id;
    private string _name;

    public Product(int id, string name)
    {
        _id = id;
        _name = name;
    }
}