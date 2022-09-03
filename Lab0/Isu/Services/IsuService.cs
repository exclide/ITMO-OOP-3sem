using System.Collections.Immutable;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly Dictionary<int, Student> _students = new ();
    private readonly Dictionary<GroupName, Group> _groups = new ();

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        _groups.Add(group.GroupName, group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        var student = new Student(group.GroupName, name);
        group.AddStudent(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        if (!_students.TryGetValue(id, out Student? student))
        {
            throw new ArgumentException("Can't find student with the given id.");
        }

        return student;
    }

    public Student? FindStudent(int id)
    {
        _students.TryGetValue(id, out Student? student);
        return student;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        if (!_groups.TryGetValue(groupName, out Group? group))
        {
            throw new ArgumentException("Can't find group with the given group name.");
        }

        return group.GetStudents();
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        var studentList = _students.Values.Where(student => student.Course.Equals(courseNumber)).ToImmutableList();
        return studentList;
    }

    public Group? FindGroup(GroupName groupName)
    {
        _groups.TryGetValue(groupName, out Group? group);
        return group;
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        var groupList = _groups.Values.Where(group => group.CourseNumber.Equals(courseNumber)).ToImmutableList();
        return groupList;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Group fromGroup = _groups.Values.First(group => group.GroupName.Equals(student.GroupName));
        Group toGroup = _groups.Values.First(group => group.GroupName.Equals(newGroup.GroupName));
        student.GroupName = newGroup.GroupName;

        fromGroup.RemoveStudent(student);
        toGroup.AddStudent(student);
    }
}