namespace Mps.Domain.Report;

public class MessageReport
{
    public MessageReport(Guid id, DateTime dateCreated)
    {
        Id = id;
        DateCreated = dateCreated;
    }

    public Guid Id { get; }
    public DateTime DateCreated { get; }
}