using Shops.Entities;

namespace Shops.Util;

public class CartBuilder
{
    private Cart _cart;

    public CartBuilder()
    {
        _cart = new Cart();
    }

    public void Reset()
    {
        _cart = new Cart();
    }

    public void AddItem(BuyInfo item)
    {
        _cart.AddItem(item);
    }

    public void AddItems(IEnumerable<BuyInfo> items)
    {
        items.ToList().ForEach(AddItem);
    }

    public void AddItems(params BuyInfo[] items)
    {
        items.ToList().ForEach(AddItem);
    }

    public Cart GetCart()
    {
        return _cart;
    }
}