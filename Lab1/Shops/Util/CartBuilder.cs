using Shops.Entities;

namespace Shops.Util;

public class CartBuilder
{
    private List<BuyInfo> _buyList = new List<BuyInfo>();

    public void Reset()
    {
        _buyList = new List<BuyInfo>();
    }

    public void AddItem(BuyInfo item)
    {
        _buyList.Add(item);
    }

    public void AddItems(IEnumerable<BuyInfo> items)
    {
        foreach (BuyInfo buyInfo in items)
        {
            _buyList.Add(buyInfo);
        }
    }

    public void AddItems(params BuyInfo[] items)
    {
        foreach (BuyInfo buyInfo in items)
        {
            _buyList.Add(buyInfo);
        }
    }

    public Cart GetCart()
    {
        return new Cart(_buyList);
    }
}