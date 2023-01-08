using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.DepartmentCQ;

public class SetDepartmentBossHandler : IRequestHandler<SetDepartmentBossCommand, DepartmentDto>
{
    private readonly IDatabaseContext _context;

    public SetDepartmentBossHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<DepartmentDto> Handle(SetDepartmentBossCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DepartmentId), cancellationToken);
        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        var boss = await _context.BossEmployees.FirstOrDefaultAsync(
            x => x.Id.Equals(request.BossId), cancellationToken);
        if (boss is null)
        {
            throw new MpsAppException($"Can't find boss employee with id {request.BossId}");
        }

        department.SetDepartmentBoss(boss);
        boss.SetDepartment(department);

        await _context.SaveChangesAsync(cancellationToken);

        return department.AsDto();
    }
}