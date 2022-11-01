using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Controllers;

public class BackupController
{
    private readonly ICollection<BackupTask> _backupTasks;

    public BackupController()
    {
        _backupTasks = new List<BackupTask>();
    }

    public BackupTask AddBackupTask(IStorageAlgorithm storageAlgorithm, Repository repository, string backupTaskName)
    {
        var backupTask = new BackupTask(storageAlgorithm, repository, backupTaskName, _backupTasks.Count);
        _backupTasks.Add(backupTask);
        return backupTask;
    }

    public Repository CreateRepository(IFileSystem fileSystem, string repositoryRootPath)
    {
        return new Repository(fileSystem, repositoryRootPath);
    }

    public RestorePoint AddRestorePointForBackupTask(BackupTask backupTask)
    {
        ArgumentNullException.ThrowIfNull(backupTask);

        var restorePoint = backupTask.CreateRestorePoint();
        return restorePoint;
    }

    public void AddBackupObjectForBackupTask(BackupTask backupTask, BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupTask);
        ArgumentNullException.ThrowIfNull(backupObject);

        backupTask.TrackObject(backupObject);
    }

    public void RemoveBackupObjectForBackupTask(BackupTask backupTask, BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupTask);
        ArgumentNullException.ThrowIfNull(backupObject);

        backupTask.UntrackObject(backupObject);
    }
}