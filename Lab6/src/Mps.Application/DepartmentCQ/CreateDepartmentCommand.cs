using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;

namespace Mps.Application.DepartmentCQ;

public record CreateDepartmentCommand(DepartmentName DepartmentName) : IRequest<DepartmentDto>;