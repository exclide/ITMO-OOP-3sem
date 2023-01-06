using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.ReportCQ;

public record GetReportsFromDateQuery(Guid DepartmentId, DateTime FromDate) : IRequest<IReadOnlyCollection<ReportDto>>;