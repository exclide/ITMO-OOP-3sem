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
        foreach (BuyInfo buyInfo in items)
        {
            _cart.AddItem(buyInfo);
        }
    }

    public void AddItems(params BuyInfo[] items)
    {
        foreach (BuyInfo buyInfo in items)
        {
            _cart.AddItem(buyInfo);
        }
    }

    public Cart GetCart()
    {
        return _cart;
    }
}