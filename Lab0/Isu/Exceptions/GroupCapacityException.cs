namespace Isu.Exceptions;

public class GroupCapacityException : Exception
{
    public GroupCapacityException() { }

    public GroupCapacityException(string message)
        : base(message)
    {
    }

    public GroupCapacityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}