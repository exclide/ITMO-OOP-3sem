using Backups.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

    [JsonIgnore]
    public StorageAlgorithmType StorageAlgorithmType { get; }
    [JsonIgnore]
    public RepositoryType RepositoryType { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm Algorithm { get; }

    public override string ToString()
    {
        return $"{nameof(StorageAlgorithmType)}: {StorageAlgorithmType}, {nameof(RepositoryType)}: {RepositoryType}";
    }
}