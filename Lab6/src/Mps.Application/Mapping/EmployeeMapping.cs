using Mps.Application.Dtos;
using Mps.Domain.Department;

namespace Mps.Application.Mapping;

public static class EmployeeMapping
{
    public static EmployeeDto AsDto(this Employee employee)
        => new EmployeeDto(employee.Id, employee.Account, employee.FullName, employee.EmployeeRole);
}