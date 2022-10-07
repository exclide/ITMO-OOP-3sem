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
        ArgumentNullException.ThrowIfNull(products);

        _products.AddRange(products.Except(_products));
    }

    public void ChangeProductPrice(Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (newPrice < 0)
        {
            throw ProductException.InvalidPrice(newPrice);
        }

        var foundProduct = _products.FirstOrDefault(x => x.Product.Equals(product));

        if (foundProduct is null)
        {
            throw ProductException.NotFound(product.Id);
        }

        foundProduct.Price = newPrice;
    }

    public decimal CheckIfAllExistsAndEnoughQuantity(Cart buyList, bool dontThrow = false) // dontThrow if using to find best price shop
    {
        ArgumentNullException.ThrowIfNull(buyList);

        decimal fullPrice = 0;

        foreach (var buyInfo in buyList.GetItems())
        {
            var product = _products.FirstOrDefault(t => t.Product.Equals(buyInfo.Product));

            if (product is null)
            {
                if (dontThrow)
                {
                    return -1;
                }

                throw ProductException.NotFound(buyInfo.Product.Id);
            }

            if (product.Quantity < buyInfo.Quantity)
            {
                if (dontThrow)
                {
                    return 0;
                }

                throw ProductException.NotEnoughQuantity(product.Quantity, buyInfo.Quantity);
            }

            fullPrice += product.Price * buyInfo.Quantity;
        }

        return fullPrice;
    }

    public void SellProductToClient(Client client, Cart buyList)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(buyList);

        decimal fullProductPrice = CheckIfAllExistsAndEnoughQuantity(buyList);

        if (client.Cash < fullProductPrice)
        {
            throw ClientException.NotEnoughMoney(client.Cash, fullProductPrice);
        }

        foreach (var buyInfo in buyList.GetItems())
        {
            var product = _products.First(t => t.Product.Equals(buyInfo.Product));
            product.Quantity -= buyInfo.Quantity;
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
        ArgumentNullException.ThrowIfNull(product);

        var productFound = _products.FirstOrDefault(x => x.Product.Equals(product));

        if (productFound is null)
        {
            throw ProductException.NotFound(product.Id);
        }

        return productFound;
    }

    public override int GetHashCode() => _id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as Shop);
    public bool Equals(Shop other) => other?._id.Equals(_id) ?? false;
}