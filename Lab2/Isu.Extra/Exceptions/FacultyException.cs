namespace Isu.Extra.Exceptions;

public class FacultyException : Exception
{
    public FacultyException(string message)
        : base(message)
    {
    }

    public static FacultyException InvalidPrefix(char prefix) => new FacultyException($"Prefix {prefix} is invalid");
}