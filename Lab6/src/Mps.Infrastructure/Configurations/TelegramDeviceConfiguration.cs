using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class TelegramDeviceConfiguration : IEntityTypeConfiguration<TelegramDevice>
{
    public void Configure(EntityTypeBuilder<TelegramDevice> builder)
    {
        builder.OwnsOne(x => x.TelegramName);
    }
}