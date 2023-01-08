using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.MessageCQ;

public record CreateSendPhoneMessageToDeviceCommand(
    Guid DeviceId, MessageText MessageText, PhoneNumber PhoneNumber) : IRequest<MessageDto>;