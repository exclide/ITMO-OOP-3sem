using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public class EmailMessage : MessageBase
{
    public EmailMessage(
        Guid id,
        MessageText messageText,
        Guid targetDeviceId,
        MessageState messageState,
        DateTime dateReceived,
        EmailAddress emailAddress)
        : base(id, messageText, targetDeviceId, messageState, dateReceived)
    {
        ArgumentNullException.ThrowIfNull(messageText);
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmailAddress = emailAddress;
    }

    protected EmailMessage()
    {
        EmailAddress = null!;
    }

    public EmailAddress EmailAddress { get; private set; }
    public override MessageType MessageType => MessageType.EmailMessage;
}