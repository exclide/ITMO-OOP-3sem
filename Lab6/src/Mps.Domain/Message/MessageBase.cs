using Mps.Domain.ValueObjects;

namespace Mps.Domain.Message;

public abstract class MessageBase : IEquatable<MessageBase>
{
    protected MessageBase(Guid id, MessageText message, Guid targetDeviceId, MessageState messageState, DateTime dateReceived)
    {
        Id = id;
        MessageText = message;
        TargetDeviceId = targetDeviceId;
        MessageState = messageState;
        DateReceived = dateReceived;
    }

    public Guid Id { get; }
    public MessageText MessageText { get; }
    public Guid TargetDeviceId { get; }
    public MessageState MessageState { get; private set; }
    public DateTime DateReceived { get; }

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