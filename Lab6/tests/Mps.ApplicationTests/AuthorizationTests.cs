using Microsoft.EntityFrameworkCore;
using Mps.Application.DepartmentCQ;
using Mps.Application.EmployeeCQ;
using Mps.Application.LoginCQ;
using Mps.Domain.ValueObjects;
using Mps.Infrastructure;

namespace Mps.ApplicationTests;

public class AuthorizationTests : IDisposable
{
    private readonly DatabaseContext _context;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public AuthorizationTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new DatabaseContext(dbContextOptions.Options);
    }

    [Fact]
    public async void CreateEmployee_CanLogin()
    {
        var command = new CreateEmployeeCommand("pleb", "123", new FullName("Kek", "Lol"));
        var handler = new CreateEmployeeHandler(_context);
        await handler.Handle(command, _cancellationToken);
        var loginCommand = new LoginEmployeeCommand("pleb", "123");
        var loginHandler = new LoginEmployeeHandler(_context);

        var response = await loginHandler.Handle(loginCommand, _cancellationToken);

        Assert.NotNull(response);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}