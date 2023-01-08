using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;

namespace Mps.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(x => x.BossEmployee)
            .WithMany(x => x.PlebEmployees);
        builder.HasOne(x => x.Department)
            .WithMany(x => x.PlebEmployees);

        builder.OwnsOne(x => x.Account, ch =>
        {
            ch.OwnsOne(c => c.AccountLogin);
            ch.OwnsOne(c => c.AccountPassHash);
        });

        builder.Navigation(x => x.Account).IsRequired();

        builder.OwnsOne(x => x.FullName);
    }
}