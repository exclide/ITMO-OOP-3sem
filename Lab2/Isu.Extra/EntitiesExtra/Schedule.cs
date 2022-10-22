using System.Collections;
using Isu.Extra.Exceptions;

namespace Isu.Extra.EntitiesExtra;

public class Schedule<T> : IEnumerable<Lesson<T>>
{
    private readonly List<Lesson<T>> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson<T>>();
    }

    public Schedule(IEnumerable<Lesson<T>> lessons)
    {
        ArgumentNullException.ThrowIfNull(lessons);

        _lessons = new List<Lesson<T>>(lessons);
    }

    public Schedule(params Lesson<T>[] lessons)
    {
        ArgumentNullException.ThrowIfNull(lessons);

        _lessons = new List<Lesson<T>>(lessons);
    }

    public IEnumerable<Lesson<T>> Lessons => _lessons;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Lesson<T>> GetEnumerator() => _lessons.GetEnumerator();

    internal void AddLesson(Lesson<T> lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        if (_lessons.Contains(lesson))
        {
            throw LessonException.TimeIntersect("Lesson time intersects with the schedule");
        }

        _lessons.Add(lesson);
    }

    internal void RemoveLesson(Lesson<T> lesson) // udalit po vremeni, ne uchitavaya audiotoriu (otdelniy metod v lesson dlya proverki peresch vremeni
    {
        ArgumentNullException.ThrowIfNull(lesson);

        if (!_lessons.Contains(lesson))
        {
            throw new LessonException("Lesson not in the list");
        }

        _lessons.Remove(lesson);
    }
}