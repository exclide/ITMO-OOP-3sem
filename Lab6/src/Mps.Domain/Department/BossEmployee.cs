using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class BossEmployee : Employee
{
    private readonly List<Employee> _plebEmployees = new List<Employee>();
    public BossEmployee(Guid id, Account account, FullName fullName)
        : base(id, account, fullName)
    {
        ArgumentNullException.ThrowIfNull(account);
        ArgumentNullException.ThrowIfNull(fullName);
    }

    protected BossEmployee()
    {
    }

    public virtual IReadOnlyCollection<Employee> PlebEmployees => _plebEmployees;
    public override EmployeeRole EmployeeRole => EmployeeRole.Boss;

    public void AddPlebEmployee(Employee plebEmployee)
    {
        ArgumentNullException.ThrowIfNull(plebEmployee);
        _plebEmployees.Add(plebEmployee);
    }
}