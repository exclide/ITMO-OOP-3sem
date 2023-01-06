using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.MessageCQ;

public record ReadMessageCommand(Guid MessageId) : IRequest<MessageDto>;