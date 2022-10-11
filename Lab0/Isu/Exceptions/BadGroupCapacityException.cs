namespace Isu.Exceptions;

public class BadGroupCapacityException : Exception
{
    public BadGroupCapacityException()
    : this("Group capacity should be greater or equal to current group size.") { }

    public BadGroupCapacityException(string message)
        : base(message)
    {
    }

    public BadGroupCapacityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}