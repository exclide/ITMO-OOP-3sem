using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.MessageCQ;

public record CreateSendEmailMessageToDeviceCommand(
    Guid DeviceId, MessageText MessageText, EmailAddress EmailAddress) : IRequest<MessageDto>;