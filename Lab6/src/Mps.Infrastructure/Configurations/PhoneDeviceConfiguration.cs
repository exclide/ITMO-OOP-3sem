using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class PhoneDeviceConfiguration : IEntityTypeConfiguration<PhoneDevice>
{
    public void Configure(EntityTypeBuilder<PhoneDevice> builder)
    {
        builder.OwnsOne(x => x.PhoneNumber);
    }
}