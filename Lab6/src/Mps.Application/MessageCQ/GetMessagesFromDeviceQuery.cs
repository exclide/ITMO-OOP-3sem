using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.MessageCQ;

public record GetMessagesFromDeviceQuery(Guid DeviceId) : IRequest<IReadOnlyCollection<MessageDto>>;