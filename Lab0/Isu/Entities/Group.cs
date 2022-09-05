using System.Data;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int DefaultGroupCapacity = 30;
    private readonly List<Student> _students;
    private int _maxGroupCapacity;

    public Group(
        GroupName groupName,
        int maxGroupCapacity = DefaultGroupCapacity,
        IEnumerable<Student>? students = null)
    {
        GroupName = groupName;
        CourseNumber = groupName.GetCourse();
        _students = students == null ? new List<Student>() : new List<Student>(students);
        MaxGroupCapacity = maxGroupCapacity;
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }

    public int MaxGroupCapacity
    {
        get => _maxGroupCapacity;
        set
        {
            if (value < _students.Count)
            {
                throw new BadGroupCapacityException();
            }

            _maxGroupCapacity = value;
        }
    }

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