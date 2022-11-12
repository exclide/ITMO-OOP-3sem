using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    void CreateRestorePoint();
    void DeleteRestorePoint(RestorePoint restorePoint);
    void TrackObject(BackupObject backupObject);
    void UntrackObject(BackupObject backupObject);
}