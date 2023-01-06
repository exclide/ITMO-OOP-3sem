using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.DeviceCQ;

public record CreateAddPhoneDeviceCommand(Guid DepartmentId, PhoneNumber PhoneNumber) : IRequest<PhoneDeviceDto>;