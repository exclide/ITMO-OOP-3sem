using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.MessageCQ;

public class MessageHandler :
    IRequestHandler<ReadMessageCommand, MessageDto>,
    IRequestHandler<ProcessMessageCommand, MessageDto>
{
    private readonly IDatabaseContext _context;

    public MessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> Handle(ReadMessageCommand request, CancellationToken cancellationToken)
    {
        var message =
            await _context.Messages.FirstOrDefaultAsync(x => x.Id.Equals(request.MessageId), cancellationToken);

        if (message is null)
        {
            throw new MpsAppException($"Can't find message with id {request.MessageId}");
        }

        message.ReadMessage();
        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }

    public async Task<MessageDto> Handle(ProcessMessageCommand request, CancellationToken cancellationToken)
    {
        var message =
            await _context.Messages.FirstOrDefaultAsync(x => x.Id.Equals(request.MessageId), cancellationToken);

        if (message is null)
        {
            throw new MpsAppException($"Can't find message with id {request.MessageId}");
        }

        message.ProcessMessage();
        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }
}