using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.ReportCQ;

public record GetReportByIdQuery(Guid DepartmentId, Guid ReportId) : IRequest<ReportDto>;