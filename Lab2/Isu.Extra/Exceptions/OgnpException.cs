namespace Isu.Extra.Exceptions;

public class OgnpException : Exception
{
    public OgnpException(string message)
        : base(message)
    {
    }

    public static OgnpException ScheduleIntersect(string message) => new OgnpException(message);
    public static OgnpException FacultyEqual(string message) => new OgnpException(message);
    public static OgnpException FlowCapacityReached(string message) => new OgnpException(message);
    public static OgnpException InvalidFlowCapacity(int capacity) => new OgnpException($"Capacity was {capacity}");
}