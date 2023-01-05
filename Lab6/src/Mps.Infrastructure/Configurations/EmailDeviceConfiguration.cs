using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Device;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class EmailDeviceConfiguration : IEntityTypeConfiguration<EmailDevice>
{
    public void Configure(EntityTypeBuilder<EmailDevice> builder)
    {
        builder.OwnsOne(x => x.EmailAddress);
    }
}