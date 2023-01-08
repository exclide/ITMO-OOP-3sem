using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;

namespace Mps.Infrastructure.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();
        builder.OwnsOne(x => x.MessagesProcessed);
        builder.OwnsOne(x => x.MessagesRead);
        builder.OwnsOne(x => x.MessagesTotal);
        builder.OwnsMany(x => x.MessagesCountByDevice);
    }
}