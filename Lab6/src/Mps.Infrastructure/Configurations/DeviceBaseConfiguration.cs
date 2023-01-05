using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class DeviceBaseConfiguration : IEntityTypeConfiguration<DeviceBase>
{
    public void Configure(EntityTypeBuilder<DeviceBase> builder)
    {
    }
}