using Mps.Domain.ValueObjects;

namespace Mps.Domain.Device;

public class TelegramDevice : DeviceBase
{
    public TelegramDevice(Guid id, TelegramName telegramName)
        : base(id)
    {
        TelegramName = telegramName;
    }

    private TelegramDevice()
    {
    }

    public TelegramName? TelegramName { get; }
}