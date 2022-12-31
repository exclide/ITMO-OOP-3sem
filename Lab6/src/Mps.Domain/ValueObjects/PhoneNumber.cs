using System.Text.RegularExpressions;
using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    private static readonly Regex TrimRegex = new Regex(@"[^0-9]+", RegexOptions.Compiled);
    public PhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new MpsDomainExcpetion($"{nameof(phone)} was null or empty");
        }

        ValidatePhone(phone);

        Phone = phone;
    }

    public string Phone { get; }

    public void ValidatePhone(string phone)
    {
        string trimmed = TrimRegex.Replace(phone, string.Empty);
        if (trimmed.Length != 11)
        {
            throw new MpsDomainExcpetion($"Invalid phone number format. " +
                                         $"Phone number should be 11 digits, but is {trimmed.Length}");
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Phone;
    }
}