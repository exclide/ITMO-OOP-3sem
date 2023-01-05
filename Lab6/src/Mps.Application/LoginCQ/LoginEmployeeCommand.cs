using MediatR;
using Mps.Application.Dtos;

namespace Mps.Application.LoginCQ;

public record LoginEmployeeCommand(string Login, string Password) : IRequest<EmployeeDto?>;