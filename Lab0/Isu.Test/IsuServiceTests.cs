using System.Data;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    [Theory]
    [InlineData("M3207", "Ophelia Pane")]
    [InlineData("M3204", "Hugh Jass")]
    [InlineData("M3200", "Huge Janus")]
    [InlineData("M3209", "Wayne Kerr")]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string groupName, string studentName)
    {
        var isuService = new IsuService();
        var newGroup = isuService.AddGroup(new GroupName(groupName));
        var newStudent = isuService.AddStudent(newGroup, studentName);

        var group = isuService.FindGroup(newGroup.GroupName);
        var student = isuService.FindStudent(newStudent.IsuId);

        Assert.NotNull(group);
        Assert.NotNull(student);
        Assert.Contains(student, group?.Students ?? new List<Student>());
        Assert.Equal(group, student?.Group);
        Assert.Contains(newStudent, newGroup.Students);
        Assert.Equal(newGroup, newStudent.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuService = new IsuService();
        var group = isuService.AddGroup(new GroupName("M3207"));

        for (int i = 0; i < group.MaxGroupCapacity; i++)
        {
            isuService.AddStudent(group, $"Student {i}");
        }

        Assert.Throws<GroupCapacityException>(() => isuService.AddStudent(group, $"Student {group.MaxGroupCapacity + 1}"));
    }

    [Theory]
    [InlineData("4M310")]
    [InlineData("M9999")]
    [InlineData("12345")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<GroupNameFormatException>(() => new IsuService().AddGroup(new GroupName(groupName)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isuService = new IsuService();
        var group1 = isuService.AddGroup(new GroupName("M3207"));
        var group2 = isuService.AddGroup(new GroupName("M3214"));
        var student = isuService.AddStudent(group1, "Jack Hoff");

        isuService.ChangeStudentGroup(student, group2);

        Assert.Equal(group2, student.Group);
        Assert.Contains(student, group2.Students);
        Assert.DoesNotContain(student, group1.Students);
    }
}