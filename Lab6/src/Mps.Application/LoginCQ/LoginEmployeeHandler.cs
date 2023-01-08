using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;

namespace Mps.Application.LoginCQ;

public class LoginEmployeeHandler : IRequestHandler<LoginEmployeeCommand, EmployeeDto?>
{
    private readonly IDatabaseContext _context;

    public LoginEmployeeHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto?> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = CreateEmployeeHandler.GetPasswordHash(request.Password);

        var employee = await _context.Employees.FirstOrDefaultAsync(
            x =>
            x.Account.AccountLogin.Login.Equals(request.Login)
            && x.Account.AccountPassHash.PassHash.Equals(hashedPassword),
            cancellationToken);

        return employee?.AsDto();
    }
}