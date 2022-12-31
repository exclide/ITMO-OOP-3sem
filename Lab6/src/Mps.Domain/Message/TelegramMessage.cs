using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public class TelegramMessage : MessageBase
{
    public TelegramMessage(Guid id, MessageText message, Guid targetDeviceId, MessageState messageState, DateTime dateReceived)
        : base(id, message, targetDeviceId, messageState, dateReceived)
    {
    }
}