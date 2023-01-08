using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;

namespace Mps.Infrastructure.Configurations;

public class BossEmployeeConfiguration : IEntityTypeConfiguration<BossEmployee>
{
    public void Configure(EntityTypeBuilder<BossEmployee> builder)
    {
        builder.Ignore(x => x.PlebEmployees);
        builder.HasOne(x => x.Department)
            .WithOne(x => x.DepartmentBoss);
    }
}