using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Models;

namespace Backups.Extra.LimitAlgorithms;

public class HybridAlgorithm : ILimitAlgorithm
{
    public HybridAlgorithm(IEnumerable<ILimitAlgorithm> algorithms, bool applyAll)
    {
        ArgumentNullException.ThrowIfNull(algorithms);
        Algorithms = algorithms;
        ApplyAll = applyAll;
    }

    public IEnumerable<ILimitAlgorithm> Algorithms { get; }
    public bool ApplyAll { get; }
    public LimitAlgorithmType LimitAlgorithmType => LimitAlgorithmType.HybridAlgorithm;

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        var listOfRestorePointsList = Algorithms
            .Select(algo => algo.Run(restorePoints));

        if (ApplyAll)
        {
            var pointsIntersect = listOfRestorePointsList
                .Aggregate((a, b) => a.Intersect(b));
            return pointsIntersect;
        }

        var pointsUnion = listOfRestorePointsList
            .Aggregate((a, b) => a.Union(b));
        return pointsUnion;
    }
}