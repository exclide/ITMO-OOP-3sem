using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private static int _studentCount = 0;

    public Student(CourseNumber course, GroupName groupName, string department, string name)
    {
        (IsuId, Course, GroupName, Department, Name) = (_studentCount++, course, groupName, department, name);
    }

    public Student(GroupName groupName, string name)
    {
        (IsuId, GroupName, Name, Course) = (_studentCount++, groupName, name, new CourseNumber(1));
    }

    public CourseNumber Course { get; }
    public GroupName GroupName { get; set; }
    public string? Department { get; }
    public int IsuId { get; }
    public string Name { get; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Student)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Course, GroupName, Department, IsuId, Name);
    }

    protected bool Equals(Student other)
    {
        return Course.Equals(other.Course) && GroupName.Equals(other.GroupName) && Department == other.Department &&
               IsuId == other.IsuId && Name == other.Name;
    }
}