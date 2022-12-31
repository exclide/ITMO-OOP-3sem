namespace Mps.Domain.Exceptions;

public class MpsDomainExcpetion : Exception
{
    public MpsDomainExcpetion(string message)
        : base(message)
    {
    }
}