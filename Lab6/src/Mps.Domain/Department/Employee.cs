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

    public Guid Id { get; protected set; }
    public Account Account { get; protected set; }
    public FullName FullName { get; protected set; }
    public Department? Department { get; protected set; }
    public Employee? BossEmployee { get; protected set; }

    public void SetDepartment(Department department)
    {
        ArgumentNullException.ThrowIfNull(department);
        Department = department;
    }

    public void SetBossEmployee(BossEmployee bossEmployee)
    {
        ArgumentNullException.ThrowIfNull(bossEmployee);
        BossEmployee = bossEmployee;
    }
}