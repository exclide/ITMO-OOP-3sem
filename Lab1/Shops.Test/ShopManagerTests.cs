using Shops.Entities;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTests
{
    private static ShopManager _shopManager = new ShopManager();

    [Fact]
    public void DeliverProductsToShopCanBuyProducts()
    {
        var client = _shopManager.AddClient("TestClient", 500);
        var shop = _shopManager.AddShop("TestShop", "Ulica Pushkina");
        var product1 = _shopManager.AddProduct("TestProd1");
        var product2 = _shopManager.AddProduct("TestProd2");
        var product3 = _shopManager.AddProduct("TestProd3");
        var productList = _shopManager.GenerateProductList(
            (product1, 1, 200),
            (product2, 2, 100),
            (product3, 5, 20));

        shop.AddProducts(productList);
        shop.SellProductToClient(client, (product1, 2), (product2, 3), (product3, 5));
    }

    [Fact]
    public void SetChangePriceOfProductInShop()
    {
        var shop = _shopManager.AddShop("TestShop", "Adr");
        var product1 = _shopManager.AddProduct("Product1");
        var product2 = _shopManager.AddProduct("Product2");
        shop.AddProducts((product1, 5, 2000));
        shop.AddProducts((product2, 1, 1000));

        shop.ChangeProductPrice(product1, 1);
        shop.ChangeProductPrice(product2, 4);

        var client = _shopManager.AddClient("TestClient", 500);

        // assert doesnt throw not enough money exc
    }

    [Fact]
    public void FindShopBestPricesForListOfProducts()
    {
        var shop1 = _shopManager.AddShop("Bad shop", "Adr1");
        var shop2 = _shopManager.AddShop("Test shop", "Adr2");
        var shop3 = _shopManager.AddShop("Best shop", "Adr3");
        var product1 = _shopManager.AddProduct("Product1");
        var product2 = _shopManager.AddProduct("Product2");

        shop1.AddProducts((product1, 10, 500), (product2, 10, 400));
        shop2.AddProducts((product1, 10, 50), (product2, 10, 40));
        shop3.AddProducts((product1, 10, 5), (product2, 10, 4));
        var bestShop = _shopManager.FindShopWithBestOffer((product1, 5), (product2, 5)); // null - doesn't exist

        Assert.Equal(shop3, bestShop);
    }

    [Fact]
    public void BuyListOfProductsInShop()
    {
    }
}