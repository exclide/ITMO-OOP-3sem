using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public PhoneNumber(string phone)
    {
        Phone = phone;
    }

    public string Phone { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Phone;
    }
}