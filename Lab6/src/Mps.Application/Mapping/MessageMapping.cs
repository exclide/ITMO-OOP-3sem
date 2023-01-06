using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.Message;

namespace Mps.Application.Mapping;

public static class MessageMapping
{
    public static MessageDto AsDto(this MessageBase message)
        => new MessageDto(
            message.Id,
            message.MessageText,
            message.TargetDeviceId,
            message.DateReceived,
            message.MessageState);
}