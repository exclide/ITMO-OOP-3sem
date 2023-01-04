using MediatR;
using Mps.Application.DataAccess;
using Mps.Application.DepartmentCQ;
using Mps.Application.Dtos;
using Mps.Application.Mapping;
using Mps.Domain.Department;

namespace Mps.Application.EmployeeCQ;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IDatabaseContext _context;

    public CreateEmployeeHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(Guid.NewGuid(), request.Account, request.FullName);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.AsDto();
    }
}