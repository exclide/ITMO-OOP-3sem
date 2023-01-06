using Mps.Domain.ValueObjects;

namespace Mps.Application.Dtos;

public record EmailDeviceDto(Guid Id, EmailAddress EmailAddress);