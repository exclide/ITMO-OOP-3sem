using Backups.Entities;
using Backups.Exceptions;
using Backups.Models;

namespace Backups.Controllers;

public class BackupObjectController
{
    private readonly BackupTask _backupTask;

    public BackupObjectController(BackupTask backupTask)
    {
        ArgumentNullException.ThrowIfNull(backupTask);
        _backupTask = backupTask;
    }

    public void TrackBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupTask.TrackObject(backupObject);
    }

    public void UntrackBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupTask.UntrackObject(backupObject);
    }

    public void CreateRestorePoint() => _backupTask.CreateRestorePoint();

    public int GetRestorePointCount() => _backupTask.Backup.RestorePoints.Count();

    public int GetStorageCount()
    {
        return _backupTask.Backup.RestorePoints.SelectMany(x => x.Storages).Count();
    }

    public IEnumerable<Storage> GetStoragesForRestorePointNumber(int restorePointNumber)
    {
        RestorePoint restorePoint = _backupTask.Backup.RestorePoints
            .FirstOrDefault(x => x.RestorePointNumber.Equals(restorePointNumber));

        if (restorePoint is null)
        {
            throw new BackupException(
                $"Can't find restore point with id {restorePointNumber} for backup {_backupTask.TaskName}");
        }

        return restorePoint.Storages;
    }

    public Backup GetBackup() => _backupTask.Backup;
    public Config GetConfig() => _backupTask.Config;
}