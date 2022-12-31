using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class MessageText : ValueObject
{
    public MessageText(string text)
    {
        Text = text;
    }

    public string Text { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}