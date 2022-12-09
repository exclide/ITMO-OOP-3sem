using Backups.Controllers;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Contexts;
using Backups.Extra.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Loggers;
using Backups.Models;

namespace Backups.Extra.Controllers;

public class BackupTasksExtraController
{
    private readonly BackupTaskContext _backupTaskContext;

    public BackupTasksExtraController(BackupTaskContext backupTaskContext)
    {
        _backupTaskContext = backupTaskContext;
    }

    public BackupTaskExtra AddBackupTaskExtra(Config config, ILogger logger, ILimitAlgorithm limitAlgorithm, string backupTaskName)
    {
        var backupTaskExtra = new BackupTaskExtra(
            new BackupTask(config, backupTaskName, _backupTaskContext.BackupTaskExtras.Count),
            logger,
            limitAlgorithm);

        _backupTaskContext.AddTask(backupTaskExtra);
        return backupTaskExtra;
    }

    public void DeleteBackupTaskExtra(BackupTaskExtra backupTaskExtra)
    {
        var task = _backupTaskContext.BackupTaskExtras.FirstOrDefault(t => t.Id == backupTaskExtra.Id);
        if (task is null)
        {
            throw new BackupException($"Delete failed, can't find task with the given id {backupTaskExtra.Id}");
        }

        _backupTaskContext.DeleteTask(backupTaskExtra);
        string backupTaskPath = $"{backupTaskExtra.BackupTask.Config.Repository.RootPath}" +
                                $"/{backupTaskExtra.BackupTask.TaskName}";
        backupTaskExtra.BackupTask.Config.Repository.DeleteDirectory(backupTaskPath, true);
    }

    public void DeleteAllBackupTasks()
    {
        foreach (var task in _backupTaskContext.BackupTaskExtras.ToArray())
        {
            DeleteBackupTaskExtra(task);
        }
    }
}