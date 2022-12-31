using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class MessageCountByDevice : ValueObject
{
    public MessageCountByDevice(Guid deviceId, int messageCount)
    {
        if (messageCount < 0)
        {
            throw new MpsDomainExcpetion($"{nameof(messageCount)} was less than 0");
        }

        DeviceId = deviceId;
        MessageCount = messageCount;
    }

    public Guid DeviceId { get; }
    public int MessageCount { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DeviceId;
        yield return MessageCount;
    }
}