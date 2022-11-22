namespace Banks.Commands;

public interface ICommand
{
    void Run();
    void Revert();
}