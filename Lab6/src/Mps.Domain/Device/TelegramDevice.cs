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
        TelegramName = null!;
    }

    public TelegramName TelegramName { get; private set; }
}