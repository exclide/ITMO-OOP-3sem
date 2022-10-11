namespace Isu.Exceptions;

public class BadStudentNameException : Exception
{
    public BadStudentNameException() { }

    public BadStudentNameException(string message)
        : base(message)
    {
    }

    public BadStudentNameException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}