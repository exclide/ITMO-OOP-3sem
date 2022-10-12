namespace Shops.Entities;

public class Cart
{
    private readonly List<BuyInfo> _items;

    public Cart(IEnumerable<BuyInfo> buyList)
    {
        _items = new List<BuyInfo>(buyList);
    }

    public IEnumerable<BuyInfo> GetItems()
    {
        return _items;
    }
}