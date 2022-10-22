namespace Isu.Extra.Exceptions;

public class LessonException : Exception
{
    public LessonException(string message)
        : base(message)
    {
    }

    public static LessonException InvalidAuditory(int auditory) => new LessonException($"Auditory was: {auditory}");
    public static LessonException TimeIntersect(string message) => new LessonException(message);
}