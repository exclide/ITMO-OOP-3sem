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
    }

    public EmailAddress? EmailAddress { get; }
}