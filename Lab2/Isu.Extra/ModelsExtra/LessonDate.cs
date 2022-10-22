namespace Isu.Extra.ModelsExtra;

public class LessonDate : IEquatable<LessonDate>
{
    public LessonDate(LessonDay lessonDay, LessonTime lessonTime)
    {
        ArgumentNullException.ThrowIfNull(lessonDay);
        ArgumentNullException.ThrowIfNull(lessonTime);

        LessonDay = lessonDay;
        LessonTime = lessonTime;
    }

    public LessonDay LessonDay { get; }
    public LessonTime LessonTime { get; }

    public override bool Equals(object obj) => Equals(obj as LessonDate);
    public override int GetHashCode() => HashCode.Combine(LessonDay, LessonTime);

    public bool Equals(LessonDate other)
    {
        if (other is null)
        {
            return false;
        }

        return other.LessonDay == LessonDay && other.LessonTime == LessonTime;
    }
}