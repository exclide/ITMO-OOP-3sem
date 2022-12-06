using Backups.Models;

namespace Backups.Entities;

public class RestorePoint : IEquatable<RestorePoint>
{
    public RestorePoint(
        IEnumerable<BackupObject> backupObjects,
        IEnumerable<Storage> storages,
        DateTime timeCreated,
        int restorePointNumber)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(storages);
        ArgumentNullException.ThrowIfNull(timeCreated);

        BackupObjects = new List<BackupObject>(backupObjects);
        Storages = new List<Storage>(storages);
        TimeCreated = timeCreated;
        RestorePointNumber = restorePointNumber;
    }

    public DateTime TimeCreated { get; }
    public int RestorePointNumber { get; }
    public ICollection<BackupObject> BackupObjects { get; set; }
    public ICollection<Storage> Storages { get; set; }

    public override string ToString()
    {
        return $"{nameof(BackupObjects)}: {BackupObjects}, {nameof(Storages)}: {Storages}, " +
               $"{nameof(TimeCreated)}: {TimeCreated}, {nameof(RestorePointNumber)}: {RestorePointNumber}";
    }

    public override int GetHashCode() => RestorePointNumber.GetHashCode();

    public override bool Equals(object obj) => this.Equals(obj as RestorePoint);
    public bool Equals(RestorePoint other) => other?.RestorePointNumber.Equals(RestorePointNumber) ?? false;
}