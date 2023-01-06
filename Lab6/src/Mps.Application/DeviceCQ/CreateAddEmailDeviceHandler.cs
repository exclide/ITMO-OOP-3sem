using System.Runtime.InteropServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Device;

namespace Mps.Application.DeviceCQ;

public class CreateAddEmailDeviceHandler : IRequestHandler<CreateAddEmailDeviceCommand, EmailDeviceDto>
{
    private readonly IDatabaseContext _context;

    public CreateAddEmailDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmailDeviceDto> Handle(CreateAddEmailDeviceCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DepartmentId), cancellationToken: cancellationToken);
        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        var device = new EmailDevice(Guid.NewGuid(), request.EmailAddress);
        department.AddControlledDevice(device);

        await _context.SaveChangesAsync(cancellationToken);

        return device.AsDto();
    }
}