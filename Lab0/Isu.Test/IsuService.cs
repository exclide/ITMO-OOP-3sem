using System.Data;
using Isu.Entities;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isuService = new Services.IsuService();
        var group = isuService.AddGroup(new GroupName("M3207"));
        var student = isuService.AddStudent(group, "Ophelia Pane");

        Assert.Contains(student, group.GetStudents());
        Assert.Equal(group.GroupName, student.GroupName);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuService = new Services.IsuService();
        var group = isuService.AddGroup(new GroupName("M3207"));

        for (int i = 0; i < 30; i++)
        {
            isuService.AddStudent(group, $"Student {i}");
        }

        Assert.Throws<ConstraintException>(() => isuService.AddStudent(group, "Student 31"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Services.IsuService().AddGroup(new GroupName("4M310")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isuService = new Services.IsuService();
        var firstGroupName = new GroupName("M3207");
        var secondGroupName = new GroupName("M3214");
        var group1 = isuService.AddGroup(firstGroupName);
        var group2 = isuService.AddGroup(secondGroupName);
        var student = isuService.AddStudent(group1, "Jack Hoff");

        isuService.ChangeStudentGroup(student, group2);

        Assert.Equal(secondGroupName, student.GroupName);
        Assert.Contains(student, group2.GetStudents());
        Assert.DoesNotContain(student, group1.GetStudents());
    }
}