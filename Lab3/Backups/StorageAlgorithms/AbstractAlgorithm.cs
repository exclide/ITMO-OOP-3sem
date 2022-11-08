using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public abstract class AbstractAlgorithm : IStorageAlgorithm
{
    public IEnumerable<Storage> RunAlgo(
        IRepository repository,
        IEnumerable<BackupObject> backupObjects,
        int restorePointNumber,
        string backupTaskName)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(backupObjects);
        if (string.IsNullOrEmpty(backupTaskName))
        {
            throw new BackupException($"{nameof(backupTaskName)} was null or empty");
        }

        string backupTaskDirectory = repository.PathCombine(repository.RootPath, backupTaskName);

        if (!repository.DirectoryExists(backupTaskDirectory))
        {
            repository.CreateDirectory(backupTaskDirectory);
        }

        string restorePointDir = repository.PathCombine(backupTaskDirectory, $"{restorePointNumber}");
        repository.CreateDirectory(restorePointDir);

        return RunInternal(repository, backupObjects, restorePointDir, backupTaskName);
    }

    public abstract StorageAlgorithmType GetAlgorithmType();

    public override string ToString()
    {
        return $"AlgorithmType: {GetAlgorithmType()}";
    }

    protected abstract IEnumerable<Storage> RunInternal(
        IRepository repository,
        IEnumerable<BackupObject> backupObjects,
        string restorePointDir,
        string backupTaskName);
}