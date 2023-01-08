using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class PhoneMessageConfiguration : IEntityTypeConfiguration<PhoneMessage>
{
    public void Configure(EntityTypeBuilder<PhoneMessage> builder)
    {
        builder.OwnsOne(x => x.PhoneNumber);
    }
}