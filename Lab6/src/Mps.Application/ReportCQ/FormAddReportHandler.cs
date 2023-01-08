using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.ReportCQ;

public class FormAddReportHandler : IRequestHandler<FormAddReportCommand, ReportDto>
{
    private readonly IDatabaseContext _context;

    public FormAddReportHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> Handle(FormAddReportCommand request, CancellationToken cancellationToken)
    {
        var department =
            await _context.Departments.FirstOrDefaultAsync(x => x.Id.Equals(request.DepartmentId), cancellationToken);

        if (department is null)
        {
            throw new MpsAppException($"Can't find department with id {request.DepartmentId}");
        }

        var report = department.FormAndAddReportFromMessageDate(request.MessageDate, DateTime.UtcNow);

        await _context.SaveChangesAsync(cancellationToken);

        return report.AsDto();
    }
}