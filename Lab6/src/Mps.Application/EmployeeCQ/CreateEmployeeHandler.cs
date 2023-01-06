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

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IDatabaseContext _context;

    public CreateEmployeeHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public static string GetPasswordHash(string password)
    {
        return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)))
            .Replace("-", string.Empty);
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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

        var employee = new Employee(Guid.NewGuid(), account, request.FullName);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.AsDto();
    }
}