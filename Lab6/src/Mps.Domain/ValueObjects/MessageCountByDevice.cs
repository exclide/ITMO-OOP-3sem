using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class MessageCountByDevice : ValueObject
{
    public MessageCountByDevice(Guid deviceId, int messageCount)
    {
        if (messageCount < 0)
        {
            throw new MpsDomainException($"{nameof(messageCount)} was less than 0");
        }

        DeviceId = deviceId;
        MessageCount = messageCount;
    }

    public Guid DeviceId { get; private set; }
    public int MessageCount { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DeviceId;
        yield return MessageCount;
    }
}