using Mps.Domain.Message;
using Mps.Domain.ValueObjects;

namespace Mps.Application.Dtos;

public record MessageDto(
    Guid Id,
    MessageText MessageText,
    Guid TargetDeviceId,
    DateTime DateReceived,
    MessageState MessageState);