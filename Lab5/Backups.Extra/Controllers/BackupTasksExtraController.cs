using Backups.Controllers;
using Backups.Entities;
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
}