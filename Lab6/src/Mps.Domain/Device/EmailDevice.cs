using Mps.Domain.Message;
using Mps.Domain.ValueObjects;

namespace Mps.Domain.Device;

public class EmailDevice : DeviceBase
{
    public EmailDevice(Guid id, EmailAddress emailAddress)
        : base(id)
    {
        EmailAddress = emailAddress;
    }

    private EmailDevice()
    {
        EmailAddress = null!;
    }

    public EmailAddress EmailAddress { get; private set; }
}