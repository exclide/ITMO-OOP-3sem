using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Device;

namespace Mps.Application.DeviceCQ;

public class CreateAddTelegramDeviceHandler : IRequestHandler<CreateAddTelegramDeviceCommand, TelegramDeviceDto>
{
    private readonly IDatabaseContext _context;

    public CreateAddTelegramDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<TelegramDeviceDto> Handle(CreateAddTelegramDeviceCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DepartmentId), cancellationToken: cancellationToken);
        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        var device = new TelegramDevice(Guid.NewGuid(), request.TelegramName);
        department.AddControlledDevice(device);

        await _context.SaveChangesAsync(cancellationToken);

        return device.AsDto();
    }
}