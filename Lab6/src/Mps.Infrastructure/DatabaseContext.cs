using Microsoft.EntityFrameworkCore;
using Mps.Application.DataAccess;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Infrastructure;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        ////Database.EnsureCreated();
    }

    public DbSet<Employee> Employees { get; private init; } = null!;
    public DbSet<Department> Departments { get; private init; } = null!;
    public DbSet<Report> Reports { get; private init; } = null!;
    public DbSet<DeviceBase> Devices { get; private init; } = null!;
    public DbSet<MessageBase> Messages { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}