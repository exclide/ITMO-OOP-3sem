using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Account
{
    public Account(Guid id, AccountLogin accountLogin, AccountPassHash accountPassHash)
    {
        Id = id;
        AccountLogin = accountLogin;
        AccountPassHash = accountPassHash;
    }

    public Guid Id { get; private set; }
    public AccountLogin AccountLogin { get; private set; }
    public AccountPassHash AccountPassHash { get; private set; }
}