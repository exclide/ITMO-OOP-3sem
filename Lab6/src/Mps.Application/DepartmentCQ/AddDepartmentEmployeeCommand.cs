using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.DepartmentCQ;

public record AddDepartmentEmployeeCommand(Guid DepartmentId, Guid EmployeeId) : IRequest<DepartmentDto>;