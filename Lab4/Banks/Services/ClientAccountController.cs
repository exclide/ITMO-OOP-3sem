using Banks.Accounts;
using Banks.Commands;

namespace Banks.Services;

public class ClientAccountController
{
    private readonly IAccount _account;
    public ClientAccountController(IAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _account = account;
    }

    public void WithdrawFromAccount(decimal withdrawAmount)
    {
        var withdrawTranscation = new WithdrawTranscation(_account, withdrawAmount);
        _account.MakeTransaction(withdrawTranscation);
    }

    public void DepositToAccount(decimal depositAmount)
    {
        var depositTranscation = new DepositTranscation(_account, depositAmount);
        _account.MakeTransaction(depositTranscation);
    }
}