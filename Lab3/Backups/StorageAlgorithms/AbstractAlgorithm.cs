using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public abstract class AbstractAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> RunAlgo(
        Repository repository,
        IEnumerable<BackupObject> backupObjects,
        int restorePointNumber,
        string backupTaskName)
    {
        string backupTaskDirectory = repository.PathCombine(repository.RootPath, backupTaskName);

        if (!repository.DirectoryExists(backupTaskDirectory))
        {
            repository.CreateDirectory(backupTaskDirectory);
        }

        string restorePointDir = repository.PathCombine(backupTaskDirectory, $"{restorePointNumber}");
        repository.CreateDirectory(restorePointDir);

        return RunInternal(repository, backupObjects, restorePointDir, backupTaskName);
    }

    protected abstract IEnumerable<Storage> RunInternal(
        Repository repository,
        IEnumerable<BackupObject> backupObjects,
        string restorePointDir,
        string backupTaskName);
}