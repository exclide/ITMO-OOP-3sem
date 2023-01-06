using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.EmployeeCQ;

public record GetControlledDevicesQuery(Guid EmployeeId) : IRequest<IReadOnlyCollection<BaseDeviceDto>>;