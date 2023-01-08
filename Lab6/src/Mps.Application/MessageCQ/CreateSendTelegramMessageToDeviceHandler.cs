using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Message;

namespace Mps.Application.MessageCQ;

public class CreateSendTelegramMessageToDeviceHandler : IRequestHandler<CreateSendTelegramMessageToDeviceCommand, MessageDto>
{
    private readonly IDatabaseContext _context;

    public CreateSendTelegramMessageToDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> Handle(CreateSendTelegramMessageToDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.TelegramDevices.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DeviceId), cancellationToken);
        if (device is null)
        {
            throw new MpsAppException($"Can't find telegram device with id {request.DeviceId}");
        }

        var message = new TelegramMessage(
            Guid.NewGuid(),
            request.MessageText,
            request.DeviceId,
            MessageState.New,
            DateTime.UtcNow,
            request.TelegramName);

        device.AddMessageForDevice(message);
        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }
}