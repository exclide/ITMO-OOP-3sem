using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IEnumerable<Storage> RunAlgo(
        IRepository repository,
        IEnumerable<BackupObject> backupObjects,
        int restorePointNumber,
        string backupTaskName);

    StorageAlgorithmType GetAlgorithmType();
}