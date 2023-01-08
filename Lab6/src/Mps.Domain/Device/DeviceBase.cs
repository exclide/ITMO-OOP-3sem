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

    protected DeviceBase()
    {
    }

    public Guid Id { get; protected set; }
    public virtual IReadOnlyCollection<MessageBase> Messages => _messages;
    public virtual DeviceType DeviceType { get; protected set; }

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