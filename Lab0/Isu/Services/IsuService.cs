using System.Collections.Immutable;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Student> _students = new ();
    private readonly List<Group> _groups = new ();

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        var student = new Student(group, name);
        group.AddStudent(student);
        _students.Add(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        var student = _students.First(student => student.IsuId == id);
        return student;
    }

    public Student? FindStudent(int id)
    {
        var student = _students.FirstOrDefault(student => student.IsuId == id);
        return student;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        var group = _groups.First(group => group.GroupName.Equals(groupName));
        return group.Students;
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        var students = _students.Where(student => student.CourseNumber.Equals(courseNumber)).ToImmutableList();
        return students;
    }

    public Group? FindGroup(GroupName groupName)
    {
        var group = _groups.FirstOrDefault(group => group.GroupName.Equals(groupName));
        return group;
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = _groups.Where(group => group.CourseNumber.Equals(courseNumber)).ToImmutableList();
        return groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        var fromGroup = _groups.First(group => group.Equals(student.Group));
        var toGroup = _groups.First(group => group.Equals(newGroup));

        student.Group = newGroup;

        fromGroup.RemoveStudent(student);
        toGroup.AddStudent(student);
    }
}