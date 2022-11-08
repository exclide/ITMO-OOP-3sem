using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Models;

public class Config
{
    public Config(IRepository repository, IStorageAlgorithm algorithm)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(algorithm);

        StorageAlgorithmType = algorithm.GetAlgorithmType();
        RepositoryType = repository.GetRepositoryType();
        Repository = repository;
        Algorithm = algorithm;
    }

    public StorageAlgorithmType StorageAlgorithmType { get; }
    public RepositoryType RepositoryType { get; }
    public IStorageAlgorithm Algorithm { get; }
    public IRepository Repository { get; }
}