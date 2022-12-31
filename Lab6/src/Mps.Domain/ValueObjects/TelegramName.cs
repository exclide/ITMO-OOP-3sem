using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class TelegramName : ValueObject
{
    public TelegramName(string telegramNick)
    {
        TelegramNick = telegramNick;
    }

    public string TelegramNick { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TelegramNick;
    }
}