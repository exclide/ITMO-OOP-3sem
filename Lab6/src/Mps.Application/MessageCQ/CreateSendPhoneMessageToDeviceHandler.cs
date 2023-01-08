using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Message;

namespace Mps.Application.MessageCQ;

public class CreateSendPhoneMessageToDeviceHandler : IRequestHandler<CreateSendPhoneMessageToDeviceCommand, MessageDto>
{
    private readonly IDatabaseContext _context;

    public CreateSendPhoneMessageToDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> Handle(CreateSendPhoneMessageToDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.PhoneDevices.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DeviceId), cancellationToken);
        if (device is null)
        {
            throw new MpsAppException($"Can't find phone device with id {request.DeviceId}");
        }

        var message = new PhoneMessage(
            Guid.NewGuid(),
            request.MessageText,
            request.DeviceId,
            MessageState.New,
            DateTime.UtcNow,
            request.PhoneNumber);

        device.AddMessageForDevice(message);
        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }
}