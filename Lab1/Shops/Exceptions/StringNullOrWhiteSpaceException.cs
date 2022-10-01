namespace Shops.Exceptions;

public class StringNullOrWhiteSpaceException : Exception
{
    public StringNullOrWhiteSpaceException(string message)
        : base(message)
    {
    }
}