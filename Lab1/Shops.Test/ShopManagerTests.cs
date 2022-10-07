using Shops.Entities;
using Shops.Exceptions;
using Shops.Services;
using Shops.Util;
using Xunit;

namespace Shops.Test;

public class ShopManagerTests
{
    private readonly ShopManager _shopManager = new ShopManager();
    private readonly CartBuilder _cartBuilder = new CartBuilder();

    [Theory]
    [InlineData(100, 1, 50, 2, 25, 1, 2)]
    [InlineData(10, 10, 1, 10, 2, 3, 3)]
    [InlineData(1000, 10, 100, 10, 200, 5, 2)]
    public void DeliverProductsToShopCanBuyProducts(
        int clientCash,
        int productCount1,
        decimal productPrice1,
        int productCount2,
        decimal productPrice2,
        int productBuyCount1,
        int productBuyCount2)
    {
        var client = _shopManager.AddClient("TestClient", clientCash);
        var shop = _shopManager.AddShop("TestShop", "Ulica Pushkina");
        var product1 = _shopManager.AddProduct("TestProd1");
        var product2 = _shopManager.AddProduct("TestProd2");

        shop.AddProducts(
            new ProductInfo(product1, productCount1, productPrice1),
            new ProductInfo(product2, productCount2, productPrice2));

        _cartBuilder.Reset();
        _cartBuilder.AddItem(new BuyInfo(product1, productBuyCount1));
        _cartBuilder.AddItem(new BuyInfo(product2, productBuyCount2));
        var cart = _cartBuilder.GetCart();

        shop.SellProductToClient(client, cart);

        Assert.Equal(clientCash - (productPrice1 * productBuyCount1) - (productPrice2 * productBuyCount2), client.Cash);

        Assert.Equal(productCount1 - productBuyCount1, shop.GetProductInfo(product1).Quantity);
        Assert.Equal(productCount2 - productBuyCount2, shop.GetProductInfo(product2).Quantity);
    }

    [Theory]
    [InlineData(500, 200, 50, 20)]
    [InlineData(1, 2, 100, 20)]
    [InlineData(10, 10, 10, 10)]
    public void SetChangePriceOfProductInShop(
        decimal productPrice1,
        decimal productPrice2,
        decimal newProductPrice1,
        decimal newProductPrice2)
    {
        var shop = _shopManager.AddShop("TestShop", "Adr");
        var product1 = _shopManager.AddProduct("Product1");
        var product2 = _shopManager.AddProduct("Product2");
        shop.AddProducts(new ProductInfo(product1, 1, productPrice1));
        shop.AddProducts(new ProductInfo(product2, 1, productPrice2));

        shop.ChangeProductPrice(product1, newProductPrice1);
        shop.ChangeProductPrice(product2, newProductPrice2);

        var client = _shopManager.AddClient("TestClient", 1000000);

        Assert.Equal(newProductPrice1, shop.GetProductInfo(product1).Price);
        Assert.Equal(newProductPrice2, shop.GetProductInfo(product2).Price);
    }

    [Fact]
    public void FindShopBestPricesForListOfProducts()
    {
        var shop1 = _shopManager.AddShop("Bad shop", "Adr1");
        var shop2 = _shopManager.AddShop("Test shop", "Adr2");
        var shop3 = _shopManager.AddShop("Best shop", "Adr3");
        var product1 = _shopManager.AddProduct("Product1");
        var product2 = _shopManager.AddProduct("Product2");

        shop1.AddProducts(new ProductInfo(product1, 100, 500), new ProductInfo(product2, 100, 400));
        shop2.AddProducts(new ProductInfo(product1, 100, 50), new ProductInfo(product2, 100, 40));
        shop3.AddProducts(new ProductInfo(product1, 100, 5), new ProductInfo(product2, 100, 4));

        _cartBuilder.Reset();
        _cartBuilder.AddItem(new BuyInfo(product1, 5));
        _cartBuilder.AddItem(new BuyInfo(product2, 5));
        var cart = _cartBuilder.GetCart();

        var bestShop = _shopManager.FindShopWithBestOffer(cart);

        Assert.Equal(shop3, bestShop);

        shop1.ChangeProductPrice(product1, 3);
        shop1.ChangeProductPrice(product2, 2);
        bestShop = _shopManager.FindShopWithBestOffer(cart);

        Assert.Equal(shop1, bestShop);

        _cartBuilder.Reset();
        _cartBuilder.AddItems(new BuyInfo(product1, 500), new BuyInfo(product2, 50));
        cart = _cartBuilder.GetCart();

        bestShop = _shopManager.FindShopWithBestOffer(cart);
        Assert.Null(bestShop);
    }

    [Theory]
    [InlineData(500, 400, 200)]
    [InlineData(50, 1, 50)]
    [InlineData(1, 1, 1)]
    public void ClientDoesNotHaveMoneyz_ThrowsException(decimal clientCash, decimal productPrice1, decimal productPrice2)
    {
        var client = _shopManager.AddClient("Gaylord", clientCash);
        var product1 = _shopManager.AddProduct("Product1");
        var product2 = _shopManager.AddProduct("Product2");
        var shop = _shopManager.AddShop("Shop1", "Adr");

        shop.AddProducts(new ProductInfo(product1, 10, productPrice1), new ProductInfo(product2, 10, productPrice2));

        _cartBuilder.Reset();
        _cartBuilder.AddItems(new BuyInfo(product1, 1), new BuyInfo(product2, 1));
        var cart = _cartBuilder.GetCart();
        Assert.Throws<ClientNotEnoughMoneyException>(
            () => shop.SellProductToClient(client, cart));
    }
}