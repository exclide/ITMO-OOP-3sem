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
        EmailAddress = emailAddress;
    }

    private EmailMessage()
    {
        EmailAddress = null!;
    }

    public EmailAddress EmailAddress { get; private set; }
}