using Mps.Application.Dtos;
using Mps.Domain.Department;

namespace Mps.Application.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.Id, report.DateCreated, report.MessagesTotal, report.MessagesRead, report.MessagesProcessed);
}