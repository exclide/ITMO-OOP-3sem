using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.EntitiesExtra;

public class StudentExtra : Student
{
    private readonly List<OgnpFlow> _ognpFlows;

    public StudentExtra(int isuId, GroupExtra group, string studentName)
        : base(isuId, group, studentName)
    {
        GroupExtra = group;
        _ognpFlows = new List<OgnpFlow>();
    }

    public GroupExtra GroupExtra { get; }
    public IReadOnlyCollection<OgnpFlow> OgnpFlows => _ognpFlows;

    internal void EnrollInOgnp(OgnpFlow ognpFlow)
    {
        if (_ognpFlows.Count == Config.OgnpConfig.StudentMaxOgnpFlows)
        {
            throw new OgnpException($"Student already enrolled in {Config.OgnpConfig.StudentMaxOgnpFlows} courses");
        }

        _ognpFlows.Add(ognpFlow);
    }

    internal void UnenrollFromOgnp(OgnpFlow ognpFlow)
    {
        if (!_ognpFlows.Contains(ognpFlow))
        {
            throw new OgnpException("Ognp flow doesn't contain the student");
        }

        _ognpFlows.Remove(ognpFlow);
    }
}