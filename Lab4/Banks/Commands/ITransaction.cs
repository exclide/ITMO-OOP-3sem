namespace Banks.Commands;

public interface ITransaction
{
    static int transactionCounter = 0;
    int TransactionId { get; }
    void Run();
    void Revert();
}