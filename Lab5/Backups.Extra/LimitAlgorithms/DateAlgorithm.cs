using Backups.Entities;
using Backups.Extra.Models;

namespace Backups.Extra.LimitAlgorithms;

public class DateAlgorithm : ILimitAlgorithm
{
    public DateAlgorithm(DateTime dateLimit)
    {
        ArgumentNullException.ThrowIfNull(dateLimit);
        DateLimit = dateLimit;
    }

    public DateTime DateLimit { get; }
    public LimitAlgorithmType LimitAlgorithmType => LimitAlgorithmType.DateAlgorithm;

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints;
    }
}