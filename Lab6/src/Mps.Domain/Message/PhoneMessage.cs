using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public class PhoneMessage : MessageBase
{
    public PhoneMessage(
        Guid id,
        MessageText messageText,
        Guid targetDeviceId,
        MessageState messageState,
        DateTime dateReceived,
        PhoneNumber phoneNumber)
        : base(id, messageText, targetDeviceId, messageState, dateReceived)
    {
        ArgumentNullException.ThrowIfNull(messageText);
        ArgumentNullException.ThrowIfNull(phoneNumber);
        PhoneNumber = phoneNumber;
    }

    protected PhoneMessage()
    {
        PhoneNumber = null!;
    }

    public PhoneNumber PhoneNumber { get; private set; }
    public override MessageType MessageType => MessageType.PhoneMessage;
}