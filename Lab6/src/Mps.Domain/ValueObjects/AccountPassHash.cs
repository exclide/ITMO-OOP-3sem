using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class AccountPassHash : ValueObject
{
    public AccountPassHash(string passHash)
    {
        if (string.IsNullOrWhiteSpace(passHash))
        {
            throw new MpsDomainException($"{nameof(passHash)} was null or empty");
        }

        PassHash = passHash;
    }

    public string PassHash { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PassHash;
    }
}