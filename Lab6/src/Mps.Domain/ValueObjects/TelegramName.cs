using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class TelegramName : ValueObject
{
    public TelegramName(string telegramNick)
    {
        if (string.IsNullOrWhiteSpace(telegramNick))
        {
            throw new MpsDomainExcpetion($"{nameof(telegramNick)} was null or empty");
        }

        if (telegramNick.Length is < 3 or > 20)
        {
            throw new MpsDomainExcpetion($"Telegram name too long: {telegramNick.Length}");
        }

        TelegramNick = telegramNick;
    }

    public string TelegramNick { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TelegramNick;
    }
}