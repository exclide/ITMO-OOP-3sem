using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class MessageText : ValueObject
{
    public MessageText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new MpsDomainExcpetion($"{nameof(text)} was null or empty");
        }

        if (text.Length is < 3 or > 1000)
        {
            throw new MpsDomainExcpetion($"Invalid text length: {text.Length}");
        }

        Text = text;
    }

    public string Text { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}