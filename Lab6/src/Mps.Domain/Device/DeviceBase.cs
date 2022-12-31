using Mps.Domain.Primitives;

namespace Mps.Domain.Device;

public abstract class DeviceBase
{
    protected DeviceBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public override bool Equals(object? obj) => Equals(obj as DeviceBase);
    public bool Equals(DeviceBase? other) => other?.Id.Equals(Id) ?? false;
    public override int GetHashCode() => Id.GetHashCode();
}