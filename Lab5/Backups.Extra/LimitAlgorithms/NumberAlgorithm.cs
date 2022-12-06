using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Models;

namespace Backups.Extra.LimitAlgorithms;

public class NumberAlgorithm : ILimitAlgorithm
{
    public NumberAlgorithm(int pointLimit)
    {
        if (pointLimit < 0)
        {
            throw new BackupException($"{nameof(pointLimit)} was less than 0");
        }

        PointLimit = pointLimit;
    }

    public int PointLimit { get; }
    public LimitAlgorithmType LimitAlgorithmType => LimitAlgorithmType.NumberAlgorithm;

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints;
    }
}