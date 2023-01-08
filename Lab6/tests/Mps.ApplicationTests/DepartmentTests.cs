using Microsoft.EntityFrameworkCore;
using Mps.Application.DepartmentCQ;
using Mps.Domain.ValueObjects;
using Mps.Infrastructure;

namespace Mps.ApplicationTests;

public class DepartmentTests : IDisposable
{
    private readonly DatabaseContext _context;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public DepartmentTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new DatabaseContext(dbContextOptions.Options);
    }

    [Fact]
    public async void CreateDepartment_DepartmentExists()
    {
        var command = new CreateDepartmentCommand(new DepartmentName("Test"));
        var handler = new CreateDepartmentHandler(_context);
        
        await handler.Handle(command, _cancellationToken);
        var department = _context.Departments.First(x => x.DepartmentName.Name.Equals("Test"));

        Assert.NotNull(department);
    }
    
    
    
    

    public void Dispose()
    {
        _context.Dispose();
    }
}