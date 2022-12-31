using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class BossEmployee : Employee
{
    public BossEmployee(Guid id, Account account, FullName fullName)
        : base(id, account, fullName)
    {
    }
}