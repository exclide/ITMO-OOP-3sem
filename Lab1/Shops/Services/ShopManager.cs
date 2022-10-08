using System.Collections.Immutable;
using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

public class ShopManager
{
    private readonly List<Product> _products;
    private readonly List<Shop> _shops;
    private readonly List<Client> _clients;

    private int _shopIdCounter = 0;
    private int _productIdCounter = 0;
    private int _clientIdCounter = 0;

    public ShopManager()
    {
        _products = new List<Product>();
        _shops = new List<Shop>();
        _clients = new List<Client>();
    }

    public Shop AddShop(string shopName, string shopAddress)
    {
        if (string.IsNullOrWhiteSpace(shopName))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(shopName)} was null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(shopAddress))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(shopAddress)} was null or whitespace.");
        }

        var shop = new Shop(_shopIdCounter++, shopName, shopAddress);
        _shops.Add(shop);
        return shop;
    }

    public Product AddProduct(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(productName)} was null or whitespace.");
        }

        var product = new Product(_productIdCounter++, productName);
        _products.Add(product);
        return product;
    }

    public Client AddClient(string clientName, decimal clientCash)
    {
        if (string.IsNullOrWhiteSpace(clientName))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(clientName)} was null or whitespace.");
        }

        if (clientCash < 0)
        {
            throw ClientException.InvalidCash(clientCash);
        }

        var client = new Client(_clientIdCounter++, clientName, clientCash);
        _clients.Add(client);
        return client;
    }

    public Shop FindShopWithBestOffer(Cart products)
    {
        ArgumentNullException.ThrowIfNull(products);

        return _shops.Where(shop =>
            shop.CheckIfAllExistsAndEnoughQuantity(products)).MinBy(s => s.GetFullPriceForCart(products));
    }
}