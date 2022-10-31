using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;
    private readonly List<Storage> _storages;

    public RestorePoint(List<BackupObject> backupObjects, List<Storage> storages, DateTime timeCreated)
    {
        _backupObjects = backupObjects;
        _storages = storages;
        TimeCreated = timeCreated;
    }

    public DateTime TimeCreated { get; }
    public IEnumerable<BackupObject> BackupObjects => _backupObjects;
    public IEnumerable<Storage> Storages => _storages;
}