using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.DeviceCQ;

public record CreateAddEmailDeviceCommand(Guid DepartmentId, EmailAddress EmailAddress) : IRequest<EmailDeviceDto>;