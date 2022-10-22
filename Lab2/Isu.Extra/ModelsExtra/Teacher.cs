using Isu.Extra.Exceptions;

namespace Isu.Extra.ModelsExtra;

public class Teacher
{
    public Teacher(string firstName, string secondName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(secondName))
        {
            throw new LessonException("Teacher's name was null or whitespace");
        }

        FirstName = firstName;
        SecondName = secondName;
    }

    public string FirstName { get; }
    public string SecondName { get; }
}