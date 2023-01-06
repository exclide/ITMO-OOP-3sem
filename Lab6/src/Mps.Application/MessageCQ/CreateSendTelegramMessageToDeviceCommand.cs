using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.MessageCQ;

public record CreateSendTelegramMessageToDeviceCommand(
    Guid DeviceId, MessageText MessageText, TelegramName TelegramName) : IRequest<MessageDto>;