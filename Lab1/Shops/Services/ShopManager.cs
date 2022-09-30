using Shops.Entities;

namespace Shops.Services;

public class ShopManager
{
    private static int _shopIdCounter = 0;
    private static int _productIdCounter = 0;
    private List<Product> _products;
    private List<Shop> _shops;

    public ShopManager()
    {
        _products = new List<Product>();
        _shops = new List<Shop>();
    }

    public Shop AddShop(string shopName, string shopAddress)
    {
        var shop = new Shop(_shopIdCounter++, shopName, shopAddress);
        _shops.Add(shop);
        return shop;
    }

    public Product AddProduct(string productName)
    {
        var product = new Product(_productIdCounter++, productName);
        _products.Add(product);
        return product;
    }
}