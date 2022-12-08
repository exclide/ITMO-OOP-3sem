using Backups.Entities;
using Backups.Extra.Contexts;
using Backups.Extra.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Controllers;

public class BackupObjectController : IDisposable
{
    public BackupObjectController(BackupTaskExtra backupTaskExtra, BackupTaskContext backupTaskContext)
    {
        BackupTaskExtra = backupTaskExtra;
        BackupTaskContext = backupTaskContext;
    }

    public BackupTaskExtra BackupTaskExtra { get; }
    public BackupTaskContext BackupTaskContext { get; }

    public void TrackObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        BackupTaskExtra.TrackObject(backupObject);
    }

    public void UntrackObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        BackupTaskExtra.UntrackObject(backupObject);
    }

    public void CreateRestorePoint()
    {
        BackupTaskExtra.CreateRestorePoint();
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        BackupTaskExtra.DeleteRestorePoint(restorePoint);
    }

    public void RestoreRestorePoint(RestorePoint restorePoint, string postFix = "")
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        BackupTaskExtra.RestoreRestorePoint(restorePoint, postFix);
    }

    public void RestoreRestorePoint(RestorePoint restorePoint, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(repository);
        BackupTaskExtra.RestoreRestorePoint(restorePoint, repository);
    }

    public void Dispose()
    {
        BackupTaskContext.UpdateTask(BackupTaskExtra);
    }
}