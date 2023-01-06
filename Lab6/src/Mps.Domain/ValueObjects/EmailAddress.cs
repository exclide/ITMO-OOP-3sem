using System.Net.Mail;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    public EmailAddress(string mailAddress)
    {
        MailAddress = mailAddress;
        //// MailAddress = new MailAddress(mailAddress).Address;
        //// СЛОВИЛ ЖЕСКУЮ БАГУ, НЕ ФИКСИТСЯ, НЕ ГУГЛИТСЯ, ВЫНУЖДЕННАЯ МЕРА
    }

    public string MailAddress { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MailAddress;
    }
}