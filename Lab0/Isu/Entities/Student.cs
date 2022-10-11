using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(int isuId, Group group, string studentName)
    {
        if (string.IsNullOrWhiteSpace(studentName))
        {
            throw new BadStudentNameException($"{nameof(studentName)} was null or empty");
        }

        IsuId = isuId;
        Group = group;
        Name = studentName;
        CourseNumber = group.CourseNumber;
    }

    public CourseNumber CourseNumber { get; set; }
    public Group Group { get; set; }
    public int IsuId { get; }
    public string Name { get; set; }

    public override bool Equals(object? obj) => Equals(obj as Student);
    public override int GetHashCode() => HashCode.Combine(CourseNumber, Group, IsuId, Name);
    public bool Equals(Student? other) => other?.IsuId.Equals(IsuId) ?? false;
}