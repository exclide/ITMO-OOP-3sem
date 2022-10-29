using Isu.Extra.Exceptions;
using Isu.Extra.ModelsExtra;

namespace Isu.Extra.EntitiesExtra;

public class Lesson : IEquatable<Lesson>
{
    public Lesson(LessonDate lessonDate, Teacher teacher, Auditory auditory)
    {
        ArgumentNullException.ThrowIfNull(lessonDate);
        ArgumentNullException.ThrowIfNull(teacher);
        ArgumentNullException.ThrowIfNull(auditory);

        LessonDate = lessonDate;
        Teacher = teacher;
        Auditory = auditory;
    }

    public LessonDate LessonDate { get; }
    public Teacher Teacher { get; }
    public Auditory Auditory { get; }

    public override bool Equals(object obj) => Equals(obj as Lesson);
    public override int GetHashCode() => LessonDate.GetHashCode();
    public bool Equals(Lesson other) => other?.LessonDate.Equals(LessonDate) ?? false;
}