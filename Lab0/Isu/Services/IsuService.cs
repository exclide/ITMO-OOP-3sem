using System.Collections.Immutable;
using Isu.Entities;
using Isu.Exceptions;
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
        var student = _students.FirstOrDefault(student => student.IsuId == id);
        if (student == null)
        {
            throw new StudentNotFoundException($"Student with ID {id} not found.");
        }

        return student;
    }

    public Student? FindStudent(int id)
    {
        var student = _students.FirstOrDefault(student => student.IsuId == id);
        return student;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        var group = _groups.FirstOrDefault(group => group.GroupName.Equals(groupName));
        if (group == null)
        {
            throw new GroupNotFoundException($"Group with the given group name {groupName} not found.");
        }

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
        var fromGroup = _groups.FirstOrDefault(group => group.Equals(student.Group));
        var toGroup = _groups.FirstOrDefault(group => group.Equals(newGroup));

        if (fromGroup == null)
        {
            throw new GroupNotFoundException($"Group containing the given student ID {student.IsuId} not found.");
        }

        if (toGroup == null)
        {
            throw new GroupNotFoundException($"Group with the given name {newGroup.GroupName} not found.");
        }

        student.Group = newGroup;

        fromGroup.RemoveStudent(student);
        toGroup.AddStudent(student);
    }
}