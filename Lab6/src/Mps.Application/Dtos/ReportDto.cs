using Mps.Domain.ValueObjects;

namespace Mps.Application.Dtos;

public record ReportDto(
    Guid Id,
    DateTime DateCreated,
    MessageCount MessagesTotal,
    MessageCount MessagesRead,
    MessageCount MessagesProcessed);