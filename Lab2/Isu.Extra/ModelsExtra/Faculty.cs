using System.Text.RegularExpressions;
using Isu.Extra.Exceptions;

namespace Isu.Extra.ModelsExtra;

public class Faculty
{
    private static readonly Regex GroupNameRegex = new Regex(@"^[A-Z]", RegexOptions.Compiled);
    public Faculty(string name, string facultyPrefix)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new FacultyException($"{nameof(name)} was null or whitespace");
        }

        if (!GroupNameRegex.IsMatch(facultyPrefix))
        {
            throw FacultyException.InvalidPrefix(facultyPrefix);
        }

        Name = name;
        FacultyPrefix = facultyPrefix;
    }

    public string Name { get; }
    public string FacultyPrefix { get; }
}