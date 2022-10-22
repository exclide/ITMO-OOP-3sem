using Isu.Extra.Exceptions;
using Isu.Extra.ModelsExtra;

namespace Isu.Extra.EntitiesExtra;

public class OgnpCourse : IEquatable<OgnpCourse>
{
    private readonly List<OgnpFlow> _ognpFlows;
    private readonly int _id;

    public OgnpCourse(string ognpName, Faculty faculty, int id)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        if (string.IsNullOrWhiteSpace(ognpName))
        {
            throw new OgnpException($"{nameof(ognpName)} was null or whitespace");
        }

        OgnpName = ognpName;
        Faculty = faculty;
        _ognpFlows = new List<OgnpFlow>();
        _id = id;
    }

    public string OgnpName { get; }
    public Faculty Faculty { get; }
    public IEnumerable<OgnpFlow> OgnpFlows => _ognpFlows;

    public override bool Equals(object obj) => Equals(obj as OgnpCourse);
    public override int GetHashCode() => _id.GetHashCode();
    public bool Equals(OgnpCourse other) => other?._id.Equals(_id) ?? false;

    internal void AddOgnpFlow(OgnpFlow ognpFlow)
    {
        ArgumentNullException.ThrowIfNull(ognpFlow);

        _ognpFlows.Add(ognpFlow);
    }
}