using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public interface ILimitAlgorithm
{
    IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints);
}