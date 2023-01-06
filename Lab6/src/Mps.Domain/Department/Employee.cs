using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Employee
{
    public Employee(Guid id, Account account, FullName fullName)
    {
        ArgumentNullException.ThrowIfNull(account);
        ArgumentNullException.ThrowIfNull(fullName);
        Id = id;
        Account = account;
        FullName = fullName;
    }

    protected Employee()
    {
        Account = null!;
        FullName = null!;
    }

    public Guid Id { get; protected set; }
    public Account Account { get; protected set; }
    public FullName FullName { get; protected set; }
    public virtual Department? Department { get; protected set; }
    public virtual BossEmployee? BossEmployee { get; protected set; }
    public virtual EmployeeRole EmployeeRole => EmployeeRole.Pleb;

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