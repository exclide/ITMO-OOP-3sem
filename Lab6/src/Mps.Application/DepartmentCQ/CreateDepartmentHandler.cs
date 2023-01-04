using MediatR;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Mapping;
using Mps.Domain.Department;

namespace Mps.Application.DepartmentCQ;

public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDatabaseContext _context;

    public CreateDepartmentHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(Guid.NewGuid(), request.DepartmentName);

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        return department.AsDto();
    }
}