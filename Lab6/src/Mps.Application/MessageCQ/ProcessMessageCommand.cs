using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.MessageCQ;

public record ProcessMessageCommand(Guid MessageId) : IRequest<MessageDto>;