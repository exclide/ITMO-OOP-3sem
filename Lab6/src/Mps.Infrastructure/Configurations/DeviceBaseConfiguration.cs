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
        builder.HasMany(x => x.Messages).WithOne();
        builder
            .ToTable("Devices")
            .HasDiscriminator(x => x.DeviceType)
            .HasValue<PhoneDevice>(DeviceType.PhoneDevice)
            .HasValue<EmailDevice>(DeviceType.EmailDevice)
            .HasValue<TelegramDevice>(DeviceType.TelegramDevice);
    }
}