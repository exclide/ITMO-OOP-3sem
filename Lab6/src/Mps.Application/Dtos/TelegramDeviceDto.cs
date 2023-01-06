using Mps.Domain.ValueObjects;

namespace Mps.Application.Dtos;

public record TelegramDeviceDto(Guid Id, TelegramName TelegramName);