using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models;

public class ClientName
{
    private static readonly Regex ClientNameRegex = new Regex(@"^\D+$", RegexOptions.Compiled);

    public ClientName(string firstName, string lastName)
    {
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(lastName);
        if (!ClientNameRegex.IsMatch(firstName) || !ClientNameRegex.IsMatch(lastName))
        {
            throw new BankException("Wrong client name format");
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }

    public override string ToString()
    {
        return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}";
    }
}