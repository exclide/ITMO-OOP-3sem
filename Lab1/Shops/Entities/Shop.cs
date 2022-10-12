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

        foreach (ProductInfo productInfo in products)
        {
            var foundProduct = _products.FirstOrDefault(x => x.Equals(productInfo));
            if (foundProduct is null)
            {
                _products.Add(productInfo);
            }
            else
            {
                foundProduct.Quantity += productInfo.Quantity;
            }
        }
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

    public bool CheckIfAllExistsAndEnoughQuantity(Cart buyList)
    {
        ArgumentNullException.ThrowIfNull(buyList);

        return buyList.GetItems()
            .All(buyProd => _products
            .Any(sellProd => sellProd.Product.Equals(buyProd.Product) && sellProd.Quantity >= buyProd.Quantity));
    }

    public decimal GetFullPriceForCart(Cart buyList)
    {
        ArgumentNullException.ThrowIfNull(buyList);

        return buyList.GetItems()
            .Select(buyItem =>
            new
            {
                quantity = buyItem.Quantity,
                product = _products.First(t => t.Product.Equals(buyItem.Product)),
            })
            .Select(k => k.product.Price * k.quantity)
            .Sum();
    }

    public void SellProductToClient(Client client, Cart buyList)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(buyList);

        if (!CheckIfAllExistsAndEnoughQuantity(buyList))
        {
            throw ProductException.NotFoundOrNotEnoughQuantity();
        }

        decimal fullProductPrice = GetFullPriceForCart(buyList);

        client.ProcessTransaction(fullProductPrice);

        foreach (var buyInfo in buyList.GetItems())
        {
            var product = _products.First(t => t.Product.Equals(buyInfo.Product));
            product.Quantity -= buyInfo.Quantity;
        }

        _profit += fullProductPrice;
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