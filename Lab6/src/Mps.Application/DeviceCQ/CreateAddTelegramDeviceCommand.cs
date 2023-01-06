using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.DeviceCQ;

public record CreateAddTelegramDeviceCommand(Guid DepartmentId, TelegramName TelegramName) : IRequest<TelegramDeviceDto>;