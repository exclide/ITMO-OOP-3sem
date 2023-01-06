using Mps.Domain.Message;
using Mps.Domain.ValueObjects;

namespace Mps.Domain.Device;

public class EmailDevice : DeviceBase
{
    public EmailDevice(Guid id, EmailAddress emailAddress)
        : base(id)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmailAddress = emailAddress;
    }

    protected EmailDevice()
    {
        EmailAddress = null!;
    }

    public EmailAddress EmailAddress { get; protected set; }
    public override DeviceType DeviceType => DeviceType.EmailDevice;
}