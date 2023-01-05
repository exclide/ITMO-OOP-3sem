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
        PhoneNumber = phoneNumber;
    }

    private PhoneMessage()
    {
        PhoneNumber = null!;
    }

    public PhoneNumber PhoneNumber { get; private set; }
}