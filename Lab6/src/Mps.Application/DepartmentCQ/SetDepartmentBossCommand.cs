using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.DepartmentCQ;

public record SetDepartmentBossCommand(Guid DepartmentId, Guid BossId) : IRequest<DepartmentDto>;