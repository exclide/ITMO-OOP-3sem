using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    public IEnumerable<Storage> RunAlgo(
        Repository repository,
        IEnumerable<BackupObject> backupObjects,
        int restorePointNumber,
        string backupTaskName);
}