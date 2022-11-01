namespace Backups.Entities;

public class Backup
{
    private readonly ICollection<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IEnumerable<RestorePoint> RestorePoints => _restorePoints;

    public override string ToString()
    {
        return $"{nameof(_restorePoints)}: {_restorePoints}, {nameof(RestorePoints)}: {RestorePoints}";
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }
}