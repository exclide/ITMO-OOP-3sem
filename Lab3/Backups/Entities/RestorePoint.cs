using Backups.Models;

namespace Backups.Entities;

public class RestorePoint : IEquatable<RestorePoint>
{
    private readonly ICollection<BackupObject> _backupObjects;
    private readonly ICollection<Storage> _storages;

    public RestorePoint(
        IEnumerable<BackupObject> backupObjects,
        IEnumerable<Storage> storages,
        DateTime timeCreated,
        int restorePointNumber)
    {
        _backupObjects = new List<BackupObject>(backupObjects);
        _storages = new List<Storage>(storages);
        TimeCreated = timeCreated;
        RestorePointNumber = restorePointNumber;
    }

    public DateTime TimeCreated { get; }
    public int RestorePointNumber { get; }
    public IEnumerable<BackupObject> BackupObjects => _backupObjects;
    public IEnumerable<Storage> Storages => _storages;

    public override string ToString()
    {
        return $"{nameof(_backupObjects)}: {_backupObjects}, {nameof(_storages)}: {_storages}, " +
               $"{nameof(TimeCreated)}: {TimeCreated}, {nameof(RestorePointNumber)}: {RestorePointNumber}, " +
               $"{nameof(BackupObjects)}: {BackupObjects}, {nameof(Storages)}: {Storages}";
    }

    public override int GetHashCode() => RestorePointNumber.GetHashCode();

    public override bool Equals(object obj) => this.Equals(obj as RestorePoint);
    public bool Equals(RestorePoint other) => other?.RestorePointNumber.Equals(RestorePointNumber) ?? false;
}