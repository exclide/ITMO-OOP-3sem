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
        ////Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Employee> Employees { get; private init; } = null!;
    public DbSet<BossEmployee> BossEmployees { get; private init; } = null!;
    public DbSet<Department> Departments { get; private init; } = null!;
    public DbSet<Report> Reports { get; private init; } = null!;
    public DbSet<DeviceBase> Devices { get; private init; } = null!;
    public DbSet<EmailDevice> EmailDevices { get; private init; } = null!;
    public DbSet<PhoneDevice> PhoneDevices { get; private init; } = null!;
    public DbSet<TelegramDevice> TelegramDevices { get; private init; } = null!;
    public DbSet<MessageBase> Messages { get; private init; } = null!;
    public DbSet<EmailMessage> EmailMessages { get; private init; } = null!;
    public DbSet<PhoneMessage> PhoneMessages { get; private init; } = null!;
    public DbSet<TelegramMessage> TelegramMessages { get; private init; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}