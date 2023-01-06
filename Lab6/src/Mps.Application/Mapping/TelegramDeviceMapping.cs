using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.Device;

namespace Mps.Application.Mapping;

public static class TelegramDeviceMapping
{
    public static TelegramDeviceDto AsDto(this TelegramDevice telegramDevice)
        => new TelegramDeviceDto(telegramDevice.Id, telegramDevice.TelegramName);
}