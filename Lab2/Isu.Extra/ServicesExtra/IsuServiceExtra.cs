using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.EntitiesExtra;
using Isu.Extra.Exceptions;
using Isu.Extra.ModelsExtra;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.ServicesExtra;

public class IsuServiceExtra : IsuService, IIsuServiceExtra
{
    private readonly List<OgnpCourse> _courses = new List<OgnpCourse>();
    private readonly List<OgnpFlow> _flows = new List<OgnpFlow>();
    private readonly List<GroupExtra> _groups = new List<GroupExtra>();
    private readonly List<StudentExtra> _students = new List<StudentExtra>();

    public new GroupExtra AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        var group = new GroupExtra(name);
        _groups.Add(group);
        return group;
    }

    public StudentExtra AddStudent(GroupExtra group, string name)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(name);

        var student = new StudentExtra(_students.Count, group, name);
        group.AddStudent(student);
        _students.Add(student);
        return student;
    }

    public OgnpCourse AddOgnpCourse(string ognpName, Faculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        if (string.IsNullOrWhiteSpace(ognpName))
        {
            throw new OgnpException($"{nameof(ognpName)} was null or whitespace");
        }

        var course = new OgnpCourse(ognpName, faculty, _courses.Count);
        _courses.Add(course);
        return course;
    }

    public OgnpCourse FindOgnpCourse(OgnpCourse course)
    {
        var ognpCourse = _courses.FirstOrDefault(course);

        return ognpCourse;
    }

    public OgnpFlow FindOgnpFlow(OgnpFlow flow)
    {
        var ognpFlow = _flows.FirstOrDefault(flow);

        return ognpFlow;
    }

    public OgnpFlow AddOgnpFlowToCourse(OgnpCourse ognpCourse)
    {
        ArgumentNullException.ThrowIfNull(ognpCourse);

        var flow = new OgnpFlow(Config.OgnpConfig.DefaultFlowCapacity, _flows.Count);
        ognpCourse.AddOgnpFlow(flow);
        _flows.Add(flow);
        return flow;
    }

    public void AddStudentToOgnp(OgnpCourse course, OgnpFlow flow, StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(flow);
        ArgumentNullException.ThrowIfNull(student);

        if (student.GroupExtra.FacultyPrefix.Equals(course.Faculty.FacultyPrefix))
        {
            throw OgnpException.FacultyEqual($"Faculties prefix are equal: {course.Faculty.FacultyPrefix}");
        }

        student.EnrollInOgnp(flow);
        flow.AddStudent(student);
    }

    public void RemoveStudentFromOgnp(OgnpFlow flow, StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(flow);
        ArgumentNullException.ThrowIfNull(student);

        student.UnenrollFromOgnp(flow);
        flow.RemoveStudent(student);
    }

    public void AddLessonToGroup(GroupExtra group, Lesson<GroupExtra> lesson)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(lesson);

        group.AddLesson(lesson);
    }

    public void AddLessonToOgnpFlow(OgnpFlow flow, Lesson<OgnpFlow> lesson)
    {
        ArgumentNullException.ThrowIfNull(flow);
        ArgumentNullException.ThrowIfNull(lesson);

        flow.AddLesson(lesson);
    }

    public void RemoveLessonFromGroup(GroupExtra group, Lesson<GroupExtra> lesson)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(lesson);

        group.RemoveLesson(lesson);
    }

    public void RemoveLessonFromOgnpFlow(OgnpFlow flow, Lesson<OgnpFlow> lesson)
    {
        ArgumentNullException.ThrowIfNull(flow);
        ArgumentNullException.ThrowIfNull(lesson);

        flow.RemoveLesson(lesson);
    }

    public IEnumerable<OgnpFlow> GetFlowsFromOgnp(OgnpCourse course)
    {
        ArgumentNullException.ThrowIfNull(course);

        return course.OgnpFlows;
    }

    public IEnumerable<Student> GetStudentsFromOgnpFlow(OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);

        return flow.Students;
    }

    public IEnumerable<Student> GetStudentsNotEnrolledInOgnp(GroupExtra group)
    {
        ArgumentNullException.ThrowIfNull(group);

        var studentsEnrolledInOgnp = new List<StudentExtra>();
        _flows.ForEach(flow => studentsEnrolledInOgnp.AddRange(flow.Students));

        var studentsNotEnrolledInGroup = group.Students.Except(studentsEnrolledInOgnp);

        return studentsNotEnrolledInGroup;
    }
}