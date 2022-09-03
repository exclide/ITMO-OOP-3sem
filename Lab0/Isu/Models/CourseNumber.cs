namespace Isu.Models;

public class CourseNumber : IComparable<CourseNumber>
{
    public CourseNumber(int courseNumber)
    {
        if (courseNumber is < 0 or > 6)
        {
            throw new ArgumentException("Course should be between 1 and 6");
        }

        CourseId = courseNumber;
    }

    private int CourseId { get; }
    public int CompareTo(CourseNumber? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return CourseId.CompareTo(other.CourseId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CourseNumber)obj);
    }

    public override int GetHashCode()
    {
        return CourseId;
    }

    protected bool Equals(CourseNumber other)
    {
        return CourseId == other.CourseId;
    }
}