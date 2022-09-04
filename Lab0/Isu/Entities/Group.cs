using System.Data;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int MaxStudents = 30;
    private readonly List<Student> _students = new ();

    public Group(GroupName groupName, CourseNumber courseNumber, IEnumerable<Student> students)
        : this(groupName)
    {
        _students = new List<Student>(students);
    }

    public Group(GroupName groupName, CourseNumber? courseNumber = null)
    {
        GroupName = groupName;
        CourseNumber = courseNumber ?? new CourseNumber(Course.First);
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }

    public void AddStudent(Student student)
    {
        if (_students.Contains(student)) return;
        if (GetStudentCount() >= MaxStudents)
        {
            throw new ConstraintException("Can't have more than 30 students in a group.");
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        _students.Remove(student);
    }

    public IReadOnlyCollection<Student> GetStudents()
    {
        return _students;
    }

    public int GetStudentCount()
    {
        return _students.Count;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Group)obj);
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    protected bool Equals(Group other)
    {
        return GroupName.Equals(other.GroupName);
    }
}