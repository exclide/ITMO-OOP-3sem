using Mps.Domain.ValueObjects;

namespace Mps.Domain.Device;

public class PhoneDevice : DeviceBase
{
    public PhoneDevice(Guid id, PhoneNumber phoneNumber)
        : base(id)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);
        PhoneNumber = phoneNumber;
    }

    protected PhoneDevice()
    {
        PhoneNumber = null!;
    }

    public PhoneNumber PhoneNumber { get; protected set; }
    public override DeviceType DeviceType => DeviceType.PhoneDevice;
}