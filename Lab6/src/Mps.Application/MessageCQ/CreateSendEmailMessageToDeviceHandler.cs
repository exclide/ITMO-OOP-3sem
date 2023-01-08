using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Message;

namespace Mps.Application.MessageCQ;

public class CreateSendEmailMessageToDeviceHandler : IRequestHandler<CreateSendEmailMessageToDeviceCommand, MessageDto>
{
    private readonly IDatabaseContext _context;

    public CreateSendEmailMessageToDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> Handle(CreateSendEmailMessageToDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.EmailDevices.FirstOrDefaultAsync(
            x => x.Id.Equals(request.DeviceId), cancellationToken);
        if (device is null)
        {
            throw new MpsAppException($"Can't find email device with id {request.DeviceId}");
        }

        var message = new EmailMessage(
            Guid.NewGuid(),
            request.MessageText,
            request.DeviceId,
            MessageState.New,
            DateTime.UtcNow,
            request.EmailAddress);

        device.AddMessageForDevice(message);
        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }
}