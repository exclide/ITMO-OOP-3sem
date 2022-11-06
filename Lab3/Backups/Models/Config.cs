using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Models;

public class Config
{
    public Config(StorageAlgorithm storageAlgorithm, FileSystemType repositoryType, Repository repository, IStorageAlgorithm algorithm)
    {
        StorageAlgorithm = storageAlgorithm;
        RepositoryType = repositoryType;
        Repository = repository;
        Algorithm = algorithm;
    }

    public StorageAlgorithm StorageAlgorithm { get; }
    public FileSystemType RepositoryType { get; }
    public IStorageAlgorithm Algorithm { get; }
    public Repository Repository { get; }
}