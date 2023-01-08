using Mps.Domain.ValueObjects;

namespace Mps.Web.Models;

public record SendTelegramMessageToDeviceModel(Guid DeviceId, MessageText MessageText, TelegramName TelegramName);