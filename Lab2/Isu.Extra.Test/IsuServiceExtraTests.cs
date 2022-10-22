using Isu.Extra.EntitiesExtra;
using Isu.Extra.Exceptions;
using Isu.Extra.ModelsExtra;
using Isu.Extra.ServicesExtra;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuServiceExtraTests
{
    private readonly IsuServiceExtra _isuService = new IsuServiceExtra();

    [Fact]
    public void CanAddOgnpCoursesAndOgnpCourseHasAddedFlows()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var ognpCourse2 = _isuService.AddOgnpCourse("Test", new Faculty("Test", 'T'));

        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);
        var flow2 = _isuService.AddOgnpFlowToCourse(ognpCourse2);

        Assert.NotNull(_isuService.FindOgnpCourse(ognpCourse1));
        Assert.NotNull(_isuService.FindOgnpCourse(ognpCourse2));
        Assert.NotNull(_isuService.FindOgnpFlow(flow1));
        Assert.NotNull(_isuService.FindOgnpFlow(flow2));

        var flowInCourse1 = ognpCourse1.OgnpFlows.FirstOrDefault(flow1);
        var flowInCourse2 = ognpCourse2.OgnpFlows.FirstOrDefault(flow2);

        Assert.NotNull(flowInCourse1);
        Assert.NotNull(flowInCourse2);
    }

    [Fact]
    public void StudentAbleToEnrollInOgnp()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1);

        Assert.Contains(flow1, student1.OgnpFlows);
        Assert.Contains(student1, flow1.Students);
    }

    [Fact]
    public void StudentAbleToUnenrollFromOgnp()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1);
        _isuService.RemoveStudentFromOgnp(flow1, student1);

        Assert.DoesNotContain(flow1, student1.OgnpFlows);
        Assert.DoesNotContain(student1, flow1.Students);
    }

    [Fact]
    public void ListOfStudentsNotEnrolledInOgnpCorrect()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");
        StudentExtra student2 = _isuService.AddStudent(group1, "Ivan Govnov");
        StudentExtra student3 = _isuService.AddStudent(group1, "Ivan Kozlov");
        StudentExtra student4 = _isuService.AddStudent(group1, "Ivan Debilov");

        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1);
        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student4);

        var listStudentsNotEnrolledInOgnp = _isuService.GetStudentsNotEnrolledInOgnp(group1).ToArray();

        Assert.Contains(student2, listStudentsNotEnrolledInOgnp);
        Assert.Contains(student3, listStudentsNotEnrolledInOgnp);
        Assert.DoesNotContain(student1, listStudentsNotEnrolledInOgnp);
        Assert.DoesNotContain(student4, listStudentsNotEnrolledInOgnp);
    }

    [Fact]
    public void GroupScheduleIntersectsWithOgnpSchedule_ThrowException()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        var groupLesson =
            new Lesson<GroupExtra>(new LessonDate(LessonDay.Monday, LessonTime.First), "Zubok", 1, group1);
        var ognpLesson =
            new Lesson<OgnpFlow>(new LessonDate(LessonDay.Monday, LessonTime.First), "Mayatin", 2, flow1);

        _isuService.AddLessonToGroup(group1, groupLesson);
        _isuService.AddLessonToOgnpFlow(flow1, ognpLesson);

        var exception = Assert.Throws<OgnpException>(() => _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1));
        Assert.Contains("schedule intersects", exception.Message);

        _isuService.RemoveLessonFromOgnpFlow(flow1, ognpLesson);
        var ognpLesson2 =
            new Lesson<OgnpFlow>(new LessonDate(LessonDay.Friday, LessonTime.First), "Stankevich", 100, flow1);

        _isuService.AddLessonToOgnpFlow(flow1, ognpLesson2);
        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1);
    }

    [Fact]
    public void OgnpFlowCapacityReached_ThrowsException()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        for (int i = 0; i < Config.OgnpConfig.DefaultFlowCapacity; i++)
        {
            _isuService.AddStudentToOgnp(ognpCourse1, flow1, _isuService.AddStudent(group1, $"Ivan {i}"));
        }

        var exception = Assert.Throws<OgnpException>(() =>
            _isuService.AddStudentToOgnp(ognpCourse1, flow1, _isuService.AddStudent(group1, $"Ivan {Config.OgnpConfig.DefaultFlowCapacity}")));

        Assert.Equal("Flow is at max capacity", exception.Message);
    }

    [Fact]
    public void StudentEnrollsInMoreOgnpThanPossible_ThrowsException()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("Kb", new Faculty("Kiberbez", 'K'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        var ognpCourse2 = _isuService.AddOgnpCourse("TestOgnp", new Faculty("Test", 'T'));
        var flow2 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        var ognpCourse3 = _isuService.AddOgnpCourse("TestOgnp2", new Faculty("Test2", 'R'));
        var flow3 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1);
        _isuService.AddStudentToOgnp(ognpCourse2, flow2, student1);

        var exception = Assert.Throws<OgnpException>(() => _isuService.AddStudentToOgnp(ognpCourse3, flow3, student1));
        Assert.Contains("Student already enrolled", exception.Message);
    }

    [Fact]
    public void StudentFacultyEqualsOgnpFaculty_ThrowsException()
    {
        var ognpCourse1 = _isuService.AddOgnpCourse("ISIT_Course", new Faculty("ISIT", 'M'));
        var flow1 = _isuService.AddOgnpFlowToCourse(ognpCourse1);

        GroupExtra group1 = _isuService.AddGroup(new GroupName("M3207"));
        StudentExtra student1 = _isuService.AddStudent(group1, "Ivan Ivanov");

        var exception = Assert.Throws<OgnpException>(() => _isuService.AddStudentToOgnp(ognpCourse1, flow1, student1));
        Assert.Contains("Faculties prefix are equal", exception.Message);
    }
}