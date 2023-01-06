using System.Collections.Immutable;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.ReportCQ;

public class GetReportsQueryHandler :
    IRequestHandler<GetReportsFromDateQuery, IReadOnlyCollection<ReportDto>>,
    IRequestHandler<GetReportByIdQuery, ReportDto>
{
    private readonly IDatabaseContext _context;

    public GetReportsQueryHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ReportDto>> Handle(GetReportsFromDateQuery request, CancellationToken cancellationToken)
    {
        var department =
            await _context.Departments.FirstOrDefaultAsync(x => x.Id.Equals(request.DepartmentId), cancellationToken);

        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        return department.GetReportsFromDate(request.FromDate).Select(x => x.AsDto()).ToImmutableList();
    }

    public async Task<ReportDto> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        var department =
            await _context.Departments.FirstOrDefaultAsync(x => x.Id.Equals(request.DepartmentId), cancellationToken);

        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        var report = department.GetReportBy(request.ReportId);
        if (report is null)
        {
            throw new MpsAppException($"Can't find report with id {request.ReportId}");
        }

        return report.AsDto();
    }
}