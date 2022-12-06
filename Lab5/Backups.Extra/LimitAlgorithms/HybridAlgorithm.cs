using Backups.Entities;
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
        return restorePoints;
    }
}