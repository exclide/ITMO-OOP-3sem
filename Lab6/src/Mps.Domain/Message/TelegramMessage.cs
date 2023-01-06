using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public class TelegramMessage : MessageBase
{
    public TelegramMessage(
        Guid id,
        MessageText messageText,
        Guid targetDeviceId,
        MessageState messageState,
        DateTime dateReceived,
        TelegramName telegramName)
        : base(id, messageText, targetDeviceId, messageState, dateReceived)
    {
        ArgumentNullException.ThrowIfNull(messageText);
        ArgumentNullException.ThrowIfNull(telegramName);
        TelegramName = telegramName;
    }

    protected TelegramMessage()
    {
        TelegramName = null!;
    }

    public TelegramName TelegramName { get; private set; }
    public override MessageType MessageType => MessageType.TelegramMessage;
}