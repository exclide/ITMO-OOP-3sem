using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Application.Dtos;

public record EmployeeDto(Guid Id, Account Account, FullName FullName, EmployeeRole EmployeeRole);