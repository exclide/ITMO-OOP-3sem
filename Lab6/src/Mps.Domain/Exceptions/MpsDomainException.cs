namespace Mps.Domain.Exceptions;

public class MpsDomainException : Exception
{
    public MpsDomainException(string message)
        : base(message)
    {
    }
}