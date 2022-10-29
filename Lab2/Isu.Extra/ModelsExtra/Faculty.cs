using System.Text.RegularExpressions;
using Isu.Extra.Exceptions;

namespace Isu.Extra.ModelsExtra;

public class Faculty
{
    public Faculty(string name, char facultyPrefix)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new FacultyException($"{nameof(name)} was null or whitespace");
        }

        if (!(char.IsLetter(facultyPrefix) && char.IsUpper(facultyPrefix)))
        {
            throw FacultyException.InvalidPrefix(facultyPrefix);
        }

        Name = name;
        FacultyPrefix = facultyPrefix;
    }

    public string Name { get; }
    public char FacultyPrefix { get; }
}