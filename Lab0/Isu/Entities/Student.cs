using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    private static int _studentCount = 0;

    public Student(Group group, string studentName, CourseNumber courseNumber = CourseNumber.First, string? department = null)
    {
        if (string.IsNullOrWhiteSpace(studentName))
        {
            throw new ArgumentNullException(studentName);
        }

        IsuId = _studentCount++;
        Group = group;
        Name = studentName;
        CourseNumber = courseNumber;
        Department = department;
    }

    public CourseNumber CourseNumber { get; set; }
    public Group Group { get; set; }
    public string? Department { get; set; }
    public int IsuId { get; }
    public string Name { get; set; }

    public override bool Equals(object? obj) => Equals(obj as Student);
    public override int GetHashCode() => HashCode.Combine(CourseNumber, Group, Department, IsuId, Name);
    public bool Equals(Student? other) => other?.IsuId.Equals(IsuId) ?? false;
}