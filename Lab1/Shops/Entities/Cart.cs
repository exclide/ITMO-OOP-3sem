namespace Shops.Entities;

public class Cart
{
    private List<BuyInfo> _items;

    public Cart()
    {
        _items = new List<BuyInfo>();
    }

    public void AddItem(BuyInfo item)
    {
        var foundItem = _items.FirstOrDefault(x => x.Equals(item));
        if (foundItem is null)
        {
            _items.Add(item);
        }
        else
        {
            foundItem.Quantity += item.Quantity;
        }
    }

    public IEnumerable<BuyInfo> GetItems()
    {
        return _items;
    }
}