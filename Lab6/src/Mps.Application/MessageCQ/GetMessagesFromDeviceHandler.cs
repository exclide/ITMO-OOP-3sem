using System.Collections.Immutable;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.MessageCQ;

public class GetMessagesFromDeviceHandler : IRequestHandler<GetMessagesFromDeviceQuery, IReadOnlyCollection<MessageDto>>
{
    private readonly IDatabaseContext _context;

    public GetMessagesFromDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<MessageDto>> Handle(GetMessagesFromDeviceQuery request, CancellationToken cancellationToken)
    {
        var device =
            await _context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(request.DeviceId), cancellationToken);

        if (device is null)
        {
            throw new MpsAppException($"Can't find device with id {request.DeviceId}");
        }

        return device.GetMessagesForDevice().Select(x => x.AsDto()).ToImmutableList();
    }
}