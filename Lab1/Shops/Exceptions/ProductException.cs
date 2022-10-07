namespace Shops.Exceptions;

public class ProductException : Exception
{
    private ProductException(string message)
        : base(message)
    {
    }

    public static ProductException InvalidPrice(decimal priceEntered)
    {
        return new ProductException($"Price was: {priceEntered}");
    }

    public static ProductException InvalidQuantity(int quantityEntered)
    {
        return new ProductException($"Quantity was: {quantityEntered}");
    }

    public static ProductException NotEnoughQuantity(int actualQuantity, int requstedQuantity)
    {
        return new ProductException($"Actual quantity: {actualQuantity} but requsted quantity: {requstedQuantity}");
    }

    public static ProductException NotFound(int productId)
    {
        return new ProductException($"Product with given ID: {productId} was not found");
    }
}