namespace Isu.Exceptions;

public class GroupNameFormatException : Exception
{
    public GroupNameFormatException() { }

    public GroupNameFormatException(string invalidGroupName)
        : base($"Invalid group name: {invalidGroupName}")
    {
    }

    public GroupNameFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}