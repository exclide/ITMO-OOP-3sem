namespace Isu.Extra.EntitiesExtra;

public abstract class GroupWithLessons<T>
{
    public abstract void AddLesson(T lesson);
    public abstract void RemoveLesson(T lesson);
}