using Mps.Domain.Primitives;
using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Account : ValueObject
{
    public Account(AccountLogin accountLogin, AccountPassHash accountPassHash)
    {
        AccountLogin = accountLogin;
        AccountPassHash = accountPassHash;
    }

    private Account()
    {
        AccountLogin = null!;
        AccountPassHash = null!;
    }

    public AccountLogin AccountLogin { get; private set; }
    public AccountPassHash AccountPassHash { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return AccountLogin!;
    }
}