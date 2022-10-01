using System.Collections.Immutable;
using Shops.Entities;

namespace Shops.Services;

public class ShopManager
{
    private static int _shopIdCounter = 0;
    private static int _productIdCounter = 0;
    private static int _clientIdCounter = 0;

    private List<Product> _products;
    private List<Shop> _shops;
    private List<Client> _clients;

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

    public IEnumerable<ProductInfo> GenerateProductList(params (Product product, int quantity, decimal price)[] products)
    {
        var list = products.Select(x => new ProductInfo(x.product, x.quantity, x.price)).ToImmutableList();
        return list;
    }

    public Shop? FindShopWithBestOffer(params (Product product, int count)[] products)
    {
        var bestShop = _shops.OrderBy(shop => shop.CheckIfAllExistsAndEnoughQuantity(products))
            .FirstOrDefault(shop => shop.CheckIfAllExistsAndEnoughQuantity(products) > 0);

        return bestShop;
    }
}