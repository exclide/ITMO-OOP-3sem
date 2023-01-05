using Microsoft.EntityFrameworkCore;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Application.DataAccess;

public interface IDatabaseContext
{
    DbSet<Employee> Employees { get; }
    DbSet<BossEmployee> BossEmployees { get; }
    DbSet<Department> Departments { get; }
    DbSet<Report> Reports { get; }
    DbSet<DeviceBase> Devices { get; }
    DbSet<EmailDevice> EmailDevices { get; }
    DbSet<PhoneDevice> PhoneDevices { get; }
    DbSet<TelegramDevice> TelegramDevices { get; }
    DbSet<MessageBase> Messages { get; }
    DbSet<EmailMessage> EmailMessages { get; }
    DbSet<PhoneMessage> PhoneMessages { get; }
    DbSet<TelegramMessage> TelegramMessages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}