using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private static readonly Regex GroupNameRegex = new Regex(@"^[A-Z][1-6][1-4]\d{2}\d?$", RegexOptions.Compiled);
    public GroupName(string groupName)
    {
        if (!CheckGroupNameFormat(groupName))
        {
            throw new GroupNameFormatException(groupName);
        }

        Name = groupName;
    }

    public string Name { get; }

    public CourseNumber GetCourse()
    {
        return (CourseNumber)int.Parse(Name[2].ToString());
    }

    public override bool Equals(object? obj) => Equals(obj as GroupName);
    public bool Equals(GroupName? other) => other?.Name.Equals(Name) ?? false;
    public override int GetHashCode() => Name.GetHashCode();
    private bool CheckGroupNameFormat(string groupName)
    {
        return GroupNameRegex.IsMatch(groupName);
    }
}