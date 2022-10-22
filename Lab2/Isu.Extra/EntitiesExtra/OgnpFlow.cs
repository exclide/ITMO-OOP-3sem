using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.EntitiesExtra;

public class OgnpFlow : IEquatable<OgnpFlow>
{
    private readonly List<StudentExtra> _students;
    private readonly int _id;
    private int _flowCapacity;

    public OgnpFlow(int flowCapacity, int id, Schedule<OgnpFlow> schedule = null)
    {
        if (flowCapacity < 0)
        {
            throw OgnpException.InvalidFlowCapacity(flowCapacity);
        }

        _students = new List<StudentExtra>();
        _flowCapacity = flowCapacity;
        Schedule = schedule ?? new Schedule<OgnpFlow>();
        _id = id;
    }

    public Schedule<OgnpFlow> Schedule { get; }

    public IEnumerable<StudentExtra> Students => _students;

    public override bool Equals(object obj) => Equals(obj as OgnpFlow);
    public override int GetHashCode() => _id.GetHashCode();
    public bool Equals(OgnpFlow other) => other?._id.Equals(_id) ?? false;

    internal void AddStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Contains(student))
        {
            throw new OgnpException("Student is already in the flow");
        }

        if (_students.Count >= _flowCapacity)
        {
            throw OgnpException.FlowCapacityReached("Flow is at max capacity");
        }

        var flowLessons = Schedule.Lessons.Select(lesson => lesson.LessonDate);
        var studentLessons = student.GroupExtra.GroupSchedule.Lessons.Select(lesson => lesson.LessonDate);

        if (flowLessons.Intersect(studentLessons).Any())
        {
            throw OgnpException.ScheduleIntersect("Student's group schedule intersects with Ognp schedule");
        }

        _students.Add(student);
    }

    internal void RemoveStudent(StudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (!_students.Contains(student))
        {
            throw new OgnpException("Student not in the flow");
        }

        _students.Remove(student);
    }

    internal void ChangeFlowCapacity(int newFlowCapacity)
    {
        if (newFlowCapacity < 0)
        {
            throw OgnpException.InvalidFlowCapacity(newFlowCapacity);
        }

        if (_students.Count > newFlowCapacity)
        {
            throw new OgnpException($"Student count is more than the new capacity: {newFlowCapacity}");
        }

        _flowCapacity = newFlowCapacity;
    }

    internal void AddLesson(Lesson<OgnpFlow> lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        Schedule.AddLesson(lesson);
    }

    internal void RemoveLesson(Lesson<OgnpFlow> lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        Schedule.RemoveLesson(lesson);
    }
}