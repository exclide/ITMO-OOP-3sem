using Backups.Entities;
using Backups.Exceptions;

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

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints;
    }
}