using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class FullName : ValueObject
{
    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new MpsDomainExcpetion($"{nameof(firstName)} was null or empty");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new MpsDomainExcpetion($"{nameof(lastName)} was null or empty");
        }

        if (firstName.Length is < 3 or > 20)
        {
            throw new MpsDomainExcpetion($"Invalid first name length: {firstName.Length}");
        }

        if (lastName.Length is < 3 or > 20)
        {
            throw new MpsDomainExcpetion($"Invalid last name length: {lastName.Length}");
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}