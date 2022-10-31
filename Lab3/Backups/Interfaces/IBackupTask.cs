using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    public RestorePoint AddRestorePoint();
    public void TrackObject(BackupObject backupObject);
    public void UntrackObject(BackupObject backupObject);
}