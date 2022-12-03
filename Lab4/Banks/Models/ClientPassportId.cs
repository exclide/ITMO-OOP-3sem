using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models;

public class ClientPassportId
{
    private static readonly Regex ClientPassportRegex = new Regex(@"^[0-9]{4}$", RegexOptions.Compiled);

    public ClientPassportId(string passportId)
    {
        if (!ClientPassportRegex.IsMatch(passportId))
        {
            throw new BankException("Wrong passport id format");
        }

        PassportId = passportId;
    }

    public string PassportId { get; }
}