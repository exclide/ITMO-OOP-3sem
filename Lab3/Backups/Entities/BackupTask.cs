using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly string _taskName;
    private readonly ICollection<BackupObject> _trackedObjects;
    private IStorageAlgorithm _algorithm;
    private Repository _repository;
    private Backup _backup;

    public BackupTask(IStorageAlgorithm algorithm, Repository repository, string taskName)
    {
        _algorithm = algorithm;
        _repository = repository;
        _taskName = taskName;
        _backup = new Backup();
        _trackedObjects = new List<BackupObject>();
    }

    public Backup Backup => _backup;

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
        if (!_trackedObjects.Contains(backupObject))
        {
            _trackedObjects.Add(backupObject);
        }
    }

    public void UntrackObject(BackupObject backupObject)
    {
        if (!_trackedObjects.Contains(backupObject))
        {
            throw new BackupException("k");
        }

        _trackedObjects.Remove(backupObject);
    }
}