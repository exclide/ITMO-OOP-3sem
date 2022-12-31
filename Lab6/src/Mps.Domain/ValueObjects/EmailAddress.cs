using System.Net.Mail;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    public EmailAddress(string mailAddress)
    {
        MailAddress = new MailAddress(mailAddress);
    }

    public MailAddress MailAddress { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MailAddress;
    }
}