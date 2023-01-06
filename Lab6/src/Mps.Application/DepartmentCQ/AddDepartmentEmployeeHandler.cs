using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.DepartmentCQ;

public class AddDepartmentEmployeeHandler : IRequestHandler<AddDepartmentEmployeeCommand, DepartmentDto>
{
    private readonly IDatabaseContext _context;

    public AddDepartmentEmployeeHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<DepartmentDto> Handle(AddDepartmentEmployeeCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DepartmentId), cancellationToken: cancellationToken);
        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        if (department.DepartmentBoss is null)
        {
            throw new MpsAppException(
                $"Department {department.DepartmentName.Name} needs boss to be set before adding pleb employees");
        }

        var employee = await _context.Employees.FirstOrDefaultAsync(
            x => x.Id.Equals(request.EmployeeId), cancellationToken: cancellationToken);
        if (employee is null)
        {
            throw new MpsAppException($"Can't find pleb employee with id {request.EmployeeId}");
        }

        department.AddPlebEmployee(employee);
        employee.SetDepartment(department);
        employee.SetBossEmployee(department.DepartmentBoss);

        await _context.SaveChangesAsync(cancellationToken);

        return department.AsDto();
    }
}