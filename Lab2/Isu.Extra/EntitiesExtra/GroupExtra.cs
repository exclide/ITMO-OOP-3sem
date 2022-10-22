using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.EntitiesExtra;

public class GroupExtra : Group
{
    public GroupExtra(GroupName groupName, Schedule<GroupExtra> schedule = null)
        : base(groupName)
    {
        GroupSchedule = schedule ?? new Schedule<GroupExtra>();
        FacultyPrefix = GroupName.Name[0].ToString();
    }

    public Schedule<GroupExtra> GroupSchedule { get; }
    public string FacultyPrefix { get; }

    internal void AddLesson(Lesson<GroupExtra> lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        GroupSchedule.AddLesson(lesson);
    }

    internal void RemoveLesson(Lesson<GroupExtra> lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        GroupSchedule.RemoveLesson(lesson);
    }
}