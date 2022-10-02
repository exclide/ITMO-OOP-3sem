using Shops.Exceptions;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private readonly int _id;
    private readonly List<ProductInfo> _products;
    private string _name;
    private string _address;
    private decimal _profit;

    public Shop(int id, string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(name)} was null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new StringNullOrWhiteSpaceException($"{nameof(address)} was null or whitespace.");
        }

        _id = id;
        _name = name;
        _address = address;
        _profit = 0;
        _products = new List<ProductInfo>();
    }

    public void AddProducts(params ProductInfo[] products)
    {
        _products.AddRange(products.Except(_products));
    }

    public void ChangeProductPrice(Product product, decimal newPrice)
    {
        var foundProduct = _products.FirstOrDefault(x => x.Product.Equals(product));

        if (foundProduct == null)
        {
            throw new ProductNotFoundException("Can't change price. Product not found.");
        }

        foundProduct.Price = newPrice;
    }

    public decimal CheckIfAllExistsAndEnoughQuantity(params BuyInfo[] buyList)
    {
        decimal fullPrice = 0;

        foreach (var buyInfo in buyList)
        {
            var product = _products.FirstOrDefault(t => t.Product.Equals(buyInfo.product));

            if (product == null)
            {
                return -1;
            }

            if (product.Quantity < buyInfo.quantity)
            {
                return 0;
            }

            fullPrice += product.Price * buyInfo.quantity;
        }

        return fullPrice;
    }

    public void SellProductToClient(Client client, params BuyInfo[] buyList)
    {
        decimal fullProductPrice = CheckIfAllExistsAndEnoughQuantity(buyList);

        switch (fullProductPrice)
        {
            case -1:
                throw new ProductNotFoundException($"Product not found in shop.");
            case 0:
                throw new ProductNotEnoughQuantityException($"Not enough quantity of product.");
        }

        if (client.Cash < fullProductPrice)
        {
            throw new ClientNotEnoughMoneyException($"Client doesn't have enough money. Client: {client.Cash}" +
                                                    $"Needed amount: {fullProductPrice}.");
        }

        foreach (var pair in buyList)
        {
            var product = _products.First(t => t.Product.Equals(pair.product));
            product.Quantity -= pair.quantity;
            /*
            if (product.Quantity == 0)
            {
                _products.Remove(product);
            }
            working as intended
            */
        }

        _profit += fullProductPrice;
        client.Cash -= fullProductPrice;
    }

    public ProductInfo GetProductInfo(Product product)
    {
        var productFound = _products.FirstOrDefault(x => x.Product.Equals(product));

        if (productFound == null)
        {
            throw new ProductNotFoundException("Product not found.");
        }

        return productFound;
    }

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object? obj) => Equals(obj as Shop);
    public bool Equals(Shop? other) => other?._id.Equals(_id) ?? false;
}