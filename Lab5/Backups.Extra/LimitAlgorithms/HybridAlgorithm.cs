using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class HybridAlgorithm : ILimitAlgorithm
{
    public HybridAlgorithm(IEnumerable<ILimitAlgorithm> algorithms)
    {
        ArgumentNullException.ThrowIfNull(algorithms);
        Algorithms = algorithms;
    }

    public IEnumerable<ILimitAlgorithm> Algorithms { get; }

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints;
    }
}