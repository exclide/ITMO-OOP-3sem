using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly ICollection<BackupObject> _backupObjects;
    private readonly ICollection<Storage> _storages;

    public RestorePoint(IEnumerable<BackupObject> backupObjects, IEnumerable<Storage> storages, DateTime timeCreated)
    {
        _backupObjects = new List<BackupObject>(backupObjects);
        _storages = new List<Storage>(storages);
        TimeCreated = timeCreated;
    }

    public DateTime TimeCreated { get; }
    public IEnumerable<BackupObject> BackupObjects => _backupObjects;
    public IEnumerable<Storage> Storages => _storages;
}