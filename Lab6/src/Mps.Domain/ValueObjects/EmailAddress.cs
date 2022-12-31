using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    public EmailAddress(string email)
    {
        Email = email;
    }

    public string Email { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
    }
}