using System.Data;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int DefaultGroupCapacity = 30;
    private readonly List<Student> _students;

    public Group(
        GroupName groupName,
        CourseNumber courseNumber = CourseNumber.First,
        int maxGroupCapacity = DefaultGroupCapacity,
        IEnumerable<Student>? students = null)
    {
        GroupName = groupName;
        CourseNumber = courseNumber;
        MaxGroupCapacity = maxGroupCapacity;
        _students = students == null ? new List<Student>() : new List<Student>(students);
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }
    public int MaxGroupCapacity { get; set; }
    public IReadOnlyCollection<Student> Students => _students;

    public void AddStudent(Student student)
    {
        if (_students.Contains(student)) return;
        if (Students.Count >= MaxGroupCapacity)
        {
            throw new GroupCapacityException($"Can't have more than {MaxGroupCapacity} students in a group.");
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        _students.Remove(student);
    }

    public override bool Equals(object? obj) => Equals(obj as Group);
    public override int GetHashCode() => GroupName.GetHashCode();
    public bool Equals(Group? other) => other?.GroupName.Equals(GroupName) ?? false;
}