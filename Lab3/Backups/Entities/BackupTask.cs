using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public class BackupTask : IBackupTask, IEquatable<BackupTask>
{
    private readonly string _taskName;
    private readonly ICollection<BackupObject> _trackedObjects;
    private IStorageAlgorithm _algorithm;
    private Repository _repository;
    private Backup _backup;

    public BackupTask(IStorageAlgorithm algorithm, Repository repository, string taskName, int id)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrEmpty(taskName))
        {
            throw new BackupException($"{nameof(taskName)} was null or empty");
        }

        _algorithm = algorithm;
        _repository = repository;
        _taskName = taskName;
        _backup = new Backup();
        _trackedObjects = new List<BackupObject>();
        Id = id;
    }

    public Backup Backup => _backup;
    public int Id { get; }

    public override string ToString()
    {
        return $"{nameof(_taskName)}: {_taskName}, {nameof(_trackedObjects)}: {_trackedObjects}, " +
               $"{nameof(_algorithm)}: {_algorithm}, {nameof(_repository)}: {_repository}, " +
               $"{nameof(_backup)}: {_backup}, {nameof(Backup)}: {Backup}";
    }

    public RestorePoint CreateRestorePoint()
    {
        int restorePointNumber = _backup.RestorePoints.Count();
        IEnumerable<Storage> storages = _algorithm.RunAlgo(
            _repository, _trackedObjects, restorePointNumber, _taskName);
        var restorePoint = new RestorePoint(_trackedObjects, storages, DateTime.Now, restorePointNumber);
        _backup.AddRestorePoint(restorePoint);

        return restorePoint;
    }

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