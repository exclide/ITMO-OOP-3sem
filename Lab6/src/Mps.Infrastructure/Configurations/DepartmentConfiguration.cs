using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;

namespace Mps.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        ////builder.HasOne(x => x.DepartmentBoss).WithOne(x => x.Department);
        ////builder.HasMany(x => x.PlebEmployees).WithOne(x => x.Department);
        builder.OwnsOne(x => x.DepartmentName);
    }
}