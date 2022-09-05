namespace Isu.Exceptions;

public class GroupNameFormatException : Exception
{
    public GroupNameFormatException()
        : this("Invalid group name format.") { }

    public GroupNameFormatException(string message)
        : base(message)
    {
    }

    public GroupNameFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}