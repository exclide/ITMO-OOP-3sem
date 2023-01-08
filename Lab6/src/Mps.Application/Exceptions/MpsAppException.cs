namespace Mps.Application.Exceptions;

public class MpsAppException : Exception
{
    public MpsAppException(string message)
        : base(message)
    {
    }
}