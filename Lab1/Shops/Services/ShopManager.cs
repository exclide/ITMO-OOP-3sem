using System.Collections.Immutable;
using Shops.Entities;

namespace Shops.Services;

public class ShopManager
{
    private static int _shopIdCounter = 0;
    private static int _productIdCounter = 0;
    private static int _clientIdCounter = 0;

    private readonly List<Product> _products;
    private readonly List<Shop> _shops;
    private readonly List<Client> _clients;

    public ShopManager()
    {
        _products = new List<Product>();
        _shops = new List<Shop>();
        _clients = new List<Client>();
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

    public Client AddClient(string clientName, decimal clientCash)
    {
        var client = new Client(_clientIdCounter++, clientName, clientCash);
        _clients.Add(client);
        return client;
    }

    public Shop? FindShopWithBestOffer(params BuyInfo[] products)
    {
        var bestShop = _shops.OrderBy(shop => shop.CheckIfAllExistsAndEnoughQuantity(products))
            .FirstOrDefault(shop => shop.CheckIfAllExistsAndEnoughQuantity(products) > 0);

        return bestShop;
    }
}