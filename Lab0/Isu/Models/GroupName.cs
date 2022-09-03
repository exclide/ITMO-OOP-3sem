using System.Text.RegularExpressions;

namespace Isu.Models;

public class GroupName : IComparable<GroupName>
{
    private const string GroupNameRegexPattern = "^[A-Z][1-4][1-6][0-9]{2}$";
    public GroupName(string groupName)
    {
        if (!CheckGroupNameFormat(groupName))
        {
            throw new ArgumentException("Invalid group name format.");
        }

        Name = groupName;
    }

    public string Name { get; }

    public int CompareTo(GroupName? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GroupName)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    protected bool Equals(GroupName other)
    {
        return Name == other.Name;
    }

    private bool CheckGroupNameFormat(string groupName)
    {
        var groupNameFormat = new Regex(GroupNameRegexPattern);
        return groupNameFormat.IsMatch(groupName);
    }
}