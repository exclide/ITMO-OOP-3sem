using Microsoft.EntityFrameworkCore;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Application.DataAccess;

public interface IDatabaseContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Department> Departments { get; }
    DbSet<Report> Reports { get; }
    DbSet<DeviceBase> Devices { get; }
    DbSet<MessageBase> Messages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}