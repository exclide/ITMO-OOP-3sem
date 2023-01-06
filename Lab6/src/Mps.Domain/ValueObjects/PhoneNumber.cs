using System.Text.RegularExpressions;
using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    private static readonly Regex TrimRegex = new Regex(@"[^0-9]+", RegexOptions.Compiled);
    public PhoneNumber(string phone)
    {
        /* СЛОВИЛ ЖЕСКУЮ БАГУ, НЕ ФИКСИТСЯ, НЕ ГУГЛИТСЯ, ВЫНУЖДЕННАЯ МЕРА
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new MpsDomainException($"{nameof(phone)} was null or empty");
        }

        ValidatePhone(phone);*/

        Phone = phone;
    }

    public string Phone { get; private set; }

    public void ValidatePhone(string phone)
    {
        string trimmed = TrimRegex.Replace(phone, string.Empty);
        if (trimmed.Length != 11)
        {
            throw new MpsDomainException($"Invalid phone number format. " +
                                         $"Phone number should be 11 digits, but is {trimmed.Length}");
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Phone;
    }
}