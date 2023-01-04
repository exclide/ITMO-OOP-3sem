using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Application.EmployeeCQ;

public record CreateEmployeeCommand(Account Account, FullName FullName) : IRequest<EmployeeDto>;