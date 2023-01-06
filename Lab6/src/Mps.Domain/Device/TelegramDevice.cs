using Mps.Domain.ValueObjects;

namespace Mps.Domain.Device;

public class TelegramDevice : DeviceBase
{
    public TelegramDevice(Guid id, TelegramName telegramName)
        : base(id)
    {
        ArgumentNullException.ThrowIfNull(telegramName);
        TelegramName = telegramName;
    }

    protected TelegramDevice()
    {
        TelegramName = null!;
    }

    public TelegramName TelegramName { get; protected set; }
    public override DeviceType DeviceType => DeviceType.TelegramDevice;
}