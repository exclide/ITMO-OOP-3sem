using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Report
{
    public Report(
        Guid id,
        DateTime dateCreated,
        MessageCount messagesTotal,
        MessageCount messagesRead,
        MessageCount messagesProcessed,
        IReadOnlyCollection<MessageCountByDevice> messagesCountByDevice)
    {
        Id = id;
        DateCreated = dateCreated;
        MessagesTotal = messagesTotal;
        MessagesRead = messagesRead;
        MessagesProcessed = messagesProcessed;
        MessagesCountByDevice = new List<MessageCountByDevice>(messagesCountByDevice);
    }

    public Guid Id { get; }
    public DateTime DateCreated { get; }
    public MessageCount MessagesTotal { get; }
    public MessageCount MessagesRead { get; }
    public MessageCount MessagesProcessed { get; }
    public IReadOnlyCollection<MessageCountByDevice> MessagesCountByDevice { get; }
}