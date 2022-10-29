using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.EntitiesExtra;

public class GroupExtra : Group
{
    public GroupExtra(GroupName groupName, Schedule schedule = null)
        : base(groupName)
    {
        GroupSchedule = schedule ?? new Schedule();
        FacultyPrefix = GroupName.Name[0];
    }

    public Schedule GroupSchedule { get; }
    public char FacultyPrefix { get; }

    internal void AddLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        GroupSchedule.AddLesson(lesson);
    }

    internal void RemoveLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        GroupSchedule.RemoveLesson(lesson);
    }
}