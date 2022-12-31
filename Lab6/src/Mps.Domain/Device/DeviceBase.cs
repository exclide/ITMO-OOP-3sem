using Mps.Domain.Message;
using Mps.Domain.Primitives;

namespace Mps.Domain.Device;

public abstract class DeviceBase
{
    private readonly List<MessageBase> _messages = new List<MessageBase>();
    protected DeviceBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public IReadOnlyCollection<MessageBase> Messages => _messages;

    public IReadOnlyCollection<MessageBase> GetMessagesForDevice()
    {
        return Messages;
    }

    public void AddMessageForDevice(MessageBase message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _messages.Add(message);
    }

    public override bool Equals(object? obj) => Equals(obj as DeviceBase);
    public bool Equals(DeviceBase? other) => other?.Id.Equals(Id) ?? false;
    public override int GetHashCode() => Id.GetHashCode();
}