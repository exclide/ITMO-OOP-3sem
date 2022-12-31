using Mps.Domain.Device;
using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Department
{
    private List<Employee> _plebEmployees = new List<Employee>();
    private List<DeviceBase> _controlledDevices = new List<DeviceBase>();

    public Department(Guid id, DepartmentName departmentName)
    {
        Id = id;
        DepartmentName = departmentName;
    }

    public Guid Id { get; }
    public DepartmentName DepartmentName { get; private set; }
    public BossEmployee? DepartmentBoss { get; private set; }
    public IEnumerable<Employee> PlebEmployees => _plebEmployees;
    public IEnumerable<DeviceBase> ControlledDevices => _controlledDevices;

    public void SetDepartmentBoss(BossEmployee departmentBoss)
    {
        ArgumentNullException.ThrowIfNull(departmentBoss);
        DepartmentBoss = departmentBoss;
    }

    public void AddPlebEmployee(Employee plebEmplyee)
    {
        _plebEmployees.Add(plebEmplyee);
    }

    public void AddControlledDevice(DeviceBase device)
    {
        _controlledDevices.Add(device);
    }
}