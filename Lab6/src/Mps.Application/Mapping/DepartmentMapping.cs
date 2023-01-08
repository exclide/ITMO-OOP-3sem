using Mps.Application.Dtos;
using Mps.Domain.Department;

namespace Mps.Application.Mapping;

public static class DepartmentMapping
{
    public static DepartmentDto AsDto(this Department department)
        => new DepartmentDto(department.Id, department.DepartmentName);
}