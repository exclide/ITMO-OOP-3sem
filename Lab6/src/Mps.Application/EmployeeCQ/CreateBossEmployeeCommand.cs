using MediatR;
using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Application.EmployeeCQ;

public record CreateBossEmployeeCommand(string Login, string Password, FullName FullName) : IRequest<EmployeeDto>;