namespace Banks.Commands;

public interface ITransaction
{
    void Run();
    void Revert();
}