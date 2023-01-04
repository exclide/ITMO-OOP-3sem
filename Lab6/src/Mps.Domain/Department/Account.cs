using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Account
{
    public Account(AccountLogin accountLogin, AccountPassHash accountPassHash)
    {
        AccountLogin = accountLogin;
        AccountPassHash = accountPassHash;
    }

    private Account()
    {
    }

    public AccountLogin? AccountLogin { get; private set; }
    public AccountPassHash? AccountPassHash { get; private set; }
}