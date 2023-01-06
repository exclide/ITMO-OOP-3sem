using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.ReportCQ;

public record FormAddReportCommand(Guid DepartmentId, DateTime MessageDate) : IRequest<ReportDto>;