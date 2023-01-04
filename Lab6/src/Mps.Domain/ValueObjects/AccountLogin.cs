using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class AccountLogin : ValueObject
{
    public AccountLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new MpsDomainException($"{nameof(login)} was null or empty");
        }

        if (login.Length is < 3 or > 20)
        {
            throw new MpsDomainException($"Incorrect login length: {login.Length}");
        }

        Login = login;
    }

    public string Login { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Login;
    }
}