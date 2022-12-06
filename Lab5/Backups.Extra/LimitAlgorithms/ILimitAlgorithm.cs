using Backups.Entities;
using Backups.Extra.Models;

namespace Backups.Extra.LimitAlgorithms;

public interface ILimitAlgorithm
{
    LimitAlgorithmType LimitAlgorithmType { get; }
    IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints);
}