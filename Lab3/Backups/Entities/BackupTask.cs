using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Backups.StorageAlgorithms;
using Newtonsoft.Json;

namespace Backups.Entities;

public class BackupTask : IBackupTask, IEquatable<BackupTask>
{
    [JsonProperty]
    private readonly ICollection<BackupObject> _trackedObjects;

    public BackupTask(Config config, string taskName, int id)
    {
        ArgumentNullException.ThrowIfNull(config);
        if (string.IsNullOrEmpty(taskName))
        {
            throw new BackupException($"{nameof(taskName)} was null or empty");
        }

        Config = config;
        TaskName = taskName;
        Backup = new Backup(config);
        _trackedObjects = new List<BackupObject>();
        Id = id;
    }

    public Backup Backup { get; }
    public Config Config { get; }
    public string TaskName { get; }
    public int Id { get; }

    public override string ToString()
    {
        return $"{nameof(_trackedObjects)}: {_trackedObjects}, {nameof(Backup)}: {Backup}, " +
               $"{nameof(Config)}: {Config}, {nameof(TaskName)}: {TaskName}, {nameof(Id)}: {Id}";
    }

    public void CreateRestorePoint()
    {
        int restorePointNumber = Backup.RestorePoints.Count();
        IEnumerable<Storage> storages = Config.Algorithm.RunAlgo(
            Config.Repository, _trackedObjects, restorePointNumber, TaskName);
        var restorePoint = new RestorePoint(_trackedObjects, storages, DateTime.Now, restorePointNumber);
        Backup.AddRestorePoint(restorePoint);
    }

    public void DeleteRestorePoint(RestorePoint restorePoint) => Backup.DeleteRestorePoint(restorePoint);

    public void TrackObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (!_trackedObjects.Contains(backupObject))
        {
            _trackedObjects.Add(backupObject);
        }
    }

    public void UntrackObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (!_trackedObjects.Contains(backupObject))
        {
            throw new BackupException($"Tracked objects don't contain {backupObject}");
        }

        _trackedObjects.Remove(backupObject);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object obj) => this.Equals(obj as BackupTask);
    public bool Equals(BackupTask other) => other?.Id.Equals(Id) ?? false;
}