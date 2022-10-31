using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly string _taskName;
    private readonly List<BackupObject> _trackedObjects;
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

    public RestorePoint AddRestorePoint()
    {
        return new RestorePoint(_trackedObjects, new List<Storage>(), DateTime.Now);
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

        _trackedObjects.Add(backupObject);
    }
}