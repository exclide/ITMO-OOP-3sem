using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Employee
{
    public Employee(Guid id, Account account, FullName fullName)
    {
        Id = id;
        Account = account;
        FullName = fullName;
    }

    public Guid Id { get; }
    public Account Account { get; }
    public FullName FullName { get; }
}