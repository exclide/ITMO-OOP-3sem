using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Application.DepartmentCQ;
using Mps.Application.Dtos;
using Mps.Application.Exceptions;
using Mps.Application.Mapping;
using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Application.EmployeeCQ;

public class CreateBossEmployeeHandler : IRequestHandler<CreateBossEmployeeCommand, EmployeeDto>
{
    private readonly IDatabaseContext _context;

    public CreateBossEmployeeHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDto> Handle(CreateBossEmployeeCommand request, CancellationToken cancellationToken)
    {
        var tryFindEmployee = await _context.Employees.FirstOrDefaultAsync(
            e => e.Account.AccountLogin.Login.Equals(request.Login),
            cancellationToken);
        if (tryFindEmployee is not null)
        {
            throw new MpsAppException($"Employee with login {request.Login} already exists");
        }

        var account = new Account(
            new AccountLogin(request.Login),
            new AccountPassHash(GetPasswordHash(request.Password)));

        var employee = new BossEmployee(Guid.NewGuid(), account, request.FullName);

        _context.BossEmployees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.AsDto();
    }

    private static string GetPasswordHash(string password)
    {
        return CreateEmployeeHandler.GetPasswordHash(password);
    }
}