using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mps.Domain.Department;
using Mps.Domain.Message;

namespace Mps.Infrastructure.Configurations;

public class MessageBaseConfiguration : IEntityTypeConfiguration<MessageBase>
{
    public void Configure(EntityTypeBuilder<MessageBase> builder)
    {
        builder.OwnsOne(x => x.MessageText);
        builder
            .ToTable("Messages")
            .HasDiscriminator(x => x.MessageType)
            .HasValue<PhoneMessage>(MessageType.PhoneMessage)
            .HasValue<EmailMessage>(MessageType.EmailMessage)
            .HasValue<TelegramMessage>(MessageType.TelegramMessage);
    }
}