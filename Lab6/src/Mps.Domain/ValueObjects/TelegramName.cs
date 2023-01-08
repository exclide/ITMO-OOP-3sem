using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class TelegramName : ValueObject
{
    public TelegramName(string telegramNick)
    {
        /*  СЛОВИЛ ЖЕСКУЮ БАГУ, НЕ ФИКСИТСЯ, НЕ ГУГЛИТСЯ, ВЫНУЖДЕННАЯ МЕРА
        if (string.IsNullOrWhiteSpace(telegramNick))
        {
            throw new MpsDomainException($"{nameof(telegramNick)} was null or empty");
        }

        if (telegramNick.Length is < 3 or > 20)
        {
            throw new MpsDomainException($"Telegram name too long: {telegramNick.Length}");
        }*/

        TelegramNick = telegramNick;
    }

    public string TelegramNick { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TelegramNick;
    }
}