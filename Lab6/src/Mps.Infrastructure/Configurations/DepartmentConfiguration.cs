using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.OwnsOne(x => x.DepartmentName);
        builder.HasMany(x => x.Reports).WithOne();
        builder.HasMany(x => x.ControlledDevices);
    }
}