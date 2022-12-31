using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class MessageCount : ValueObject
{
    public MessageCount(int count)
    {
        if (count < 0)
        {
            throw new MpsDomainExcpetion($"{nameof(count)} was less than 0");
        }

        Count = count;
    }

    public int Count { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Count;
    }
}