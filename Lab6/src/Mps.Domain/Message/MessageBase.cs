using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public abstract class MessageBase : IEquatable<MessageBase>
{
    protected MessageBase(
        Guid id,
        MessageText messageText,
        Guid targetDeviceId,
        MessageState messageState,
        DateTime dateReceived)
    {
        ArgumentNullException.ThrowIfNull(messageText);
        Id = id;
        MessageText = messageText;
        TargetDeviceId = targetDeviceId;
        MessageState = messageState;
        DateReceived = dateReceived;
    }

    protected MessageBase()
    {
        MessageText = null!;
    }

    public Guid Id { get; private set; }
    public MessageText MessageText { get; private set; }
    public Guid TargetDeviceId { get; private set; }
    public MessageState MessageState { get; private set; }
    public DateTime DateReceived { get; private set; }
    public virtual MessageType MessageType { get; protected set; }

    public void ReadMessage()
    {
        MessageState = MessageState.Read;
    }

    public void ProcessMessage()
    {
        MessageState = MessageState.Processed;
    }

    public override bool Equals(object? obj) => Equals(obj as MessageBase);
    public bool Equals(MessageBase? other) => other?.Id.Equals(Id) ?? false;
    public override int GetHashCode() => Id.GetHashCode();
}