using Backups.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Loggers;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : IBackupTask
{
    private BackupTask _backupTask;

    public BackupTaskExtra(Config config, ILogger logger, ILimitAlgorithm limitAlgorithm, string taskName, int id)
    {
        _backupTask = new BackupTask(config, taskName, id);
        Logger = logger;
        LimitAlgorithm = limitAlgorithm;
    }

    public ILogger Logger { get; }
    public ILimitAlgorithm LimitAlgorithm { get; }

    public void CreateRestorePoint()
    {
        Logger.Log($"Creating restore point number {_backupTask.Backup.RestorePoints.Count()} " +
                   $"with storage algorithm: {_backupTask.Config.StorageAlgorithmType} and repository " +
                   $"{_backupTask.Config.RepositoryType}");
        _backupTask.CreateRestorePoint();
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        Logger.Log($"Deleting restore point number {restorePoint.RestorePointNumber}");
        _backupTask.DeleteRestorePoint(restorePoint);
    }

    public void TrackObject(BackupObject backupObject)
    {
        Logger.Log($"Now tracking object {backupObject}");
        _backupTask.TrackObject(backupObject);
    }

    public void UntrackObject(BackupObject backupObject)
    {
        Logger.Log($"No longer tracking object {backupObject}");
        _backupTask.UntrackObject(backupObject);
    }

    public void ApplyLimits()
    {
        Logger.Log($"Applying limit algorithm");
    }

    public void MergeRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
    }

    public void RestoreRestorePoint(RestorePoint restorePoint)
    {
    }
}