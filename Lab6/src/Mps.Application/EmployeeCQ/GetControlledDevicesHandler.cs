using System.Collections.Immutable;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.EmployeeCQ;

public class GetControlledDevicesHandler : IRequestHandler<GetControlledDevicesQuery, IReadOnlyCollection<BaseDeviceDto>>
{
    private readonly IDatabaseContext _context;

    public GetControlledDevicesHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<BaseDeviceDto>> Handle(GetControlledDevicesQuery request, CancellationToken cancellationToken)
    {
        var employee =
            await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(request.EmployeeId), cancellationToken);

        if (employee is null)
        {
            throw new MpsAppException($"Can't find employee with id {request.EmployeeId}");
        }

        if (employee.Department is null)
        {
            throw new MpsAppException($"No department set for employee with id {request.EmployeeId}");
        }

        var controlledDevices = employee.Department.GetControlledDevices();

        return controlledDevices.Select(x => x.AsDto()).ToImmutableList();
    }
}