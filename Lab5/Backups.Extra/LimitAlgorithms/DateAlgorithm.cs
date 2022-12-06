using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class DateAlgorithm : ILimitAlgorithm
{
    public DateAlgorithm(DateTime dateLimit)
    {
        ArgumentNullException.ThrowIfNull(dateLimit);
        DateLimit = dateLimit;
    }

    public DateTime DateLimit { get; }

    public IEnumerable<RestorePoint> Run(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints;
    }
}