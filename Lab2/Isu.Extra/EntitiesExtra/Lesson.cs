using Isu.Extra.Exceptions;
using Isu.Extra.ModelsExtra;

namespace Isu.Extra.EntitiesExtra;

public class Lesson<T> : IEquatable<Lesson<T>>
{
    public Lesson(LessonDate lessonDate, string teacher, int auditory, T group)
    {
        ArgumentNullException.ThrowIfNull(lessonDate);
        ArgumentNullException.ThrowIfNull(group);

        if (string.IsNullOrWhiteSpace(teacher))
        {
            throw new LessonException($"{nameof(teacher)} was null or whitespace");
        }

        if (auditory < 0)
        {
            throw LessonException.InvalidAuditory(auditory);
        }

        LessonDate = lessonDate;
        Teacher = teacher;
        Auditory = auditory;
        Group = group;
    }

    public T Group { get; }
    public LessonDate LessonDate { get; }
    public string Teacher { get; }
    public int Auditory { get; }

    public override bool Equals(object obj) => Equals(obj as Lesson<T>);
    public override int GetHashCode() => LessonDate.GetHashCode();
    public bool Equals(Lesson<T> other) => other?.LessonDate.Equals(LessonDate) ?? false;
}