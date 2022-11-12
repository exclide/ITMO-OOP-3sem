using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Controllers;

public class BackupTasksController
{
    private readonly ICollection<BackupTask> _backupTasks;

    public BackupTasksController()
    {
        _backupTasks = new List<BackupTask>();
    }

    public BackupTask AddBackupTask(Config config, string backupTaskName)
    {
        var backupTask = new BackupTask(config, backupTaskName, _backupTasks.Count);
        _backupTasks.Add(backupTask);
        return backupTask;
    }

    public void RemoveBackupTask(BackupTask backupTask)
    {
        ArgumentNullException.ThrowIfNull(backupTask);

        if (!_backupTasks.Contains(backupTask))
        {
            throw new BackupException("BackupTask not found");
        }

        _backupTasks.Remove(backupTask);
    }
}