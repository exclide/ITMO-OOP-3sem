using Backups.Models;

namespace Backups.Entities;

public class Backup
{
    private readonly ICollection<RestorePoint> _restorePoints;

    public Backup(Config config)
    {
        ArgumentNullException.ThrowIfNull(config);
        Config = config;
        _restorePoints = new List<RestorePoint>();
    }

    public IEnumerable<RestorePoint> RestorePoints => _restorePoints;
    public Config Config { get; }

    public override string ToString()
    {
        return $"{nameof(_restorePoints)}: {_restorePoints}, {nameof(RestorePoints)}: {RestorePoints}";
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        _restorePoints.Add(restorePoint);
    }
}