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
        ArgumentNullException.ThrowIfNull(messagesTotal);
        ArgumentNullException.ThrowIfNull(messagesRead);
        ArgumentNullException.ThrowIfNull(messagesProcessed);
        ArgumentNullException.ThrowIfNull(messagesCountByDevice);
        Id = id;
        DateCreated = dateCreated;
        MessagesTotal = messagesTotal;
        MessagesRead = messagesRead;
        MessagesProcessed = messagesProcessed;
        MessagesCountByDevice = new List<MessageCountByDevice>(messagesCountByDevice);
    }

    protected Report()
    {
        MessagesTotal = null!;
        MessagesRead = null!;
        MessagesProcessed = null!;
        MessagesCountByDevice = null!;
    }

    public Guid Id { get; private set; }
    public DateTime DateCreated { get; private set; }
    public MessageCount MessagesTotal { get; private set; }
    public MessageCount MessagesRead { get; private set; }
    public MessageCount MessagesProcessed { get; private set; }
    public IReadOnlyCollection<MessageCountByDevice> MessagesCountByDevice { get; private set; }
}