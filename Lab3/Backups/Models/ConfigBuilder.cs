using System.Reflection;
using System.Security.Cryptography;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.StorageAlgorithms;
using Zio;
using Zio.FileSystems;

namespace Backups.Models;

public class ConfigBuilder : IDisposable
{
    private IFileSystem _fileSystem;
    private StorageAlgorithm _storageAlgorithmType;
    private FileSystemType _fileSystemType;
    private IStorageAlgorithm _algorithm;
    private string _repositoryPath;

    public ConfigBuilder SetSingleStorage()
    {
        if (_algorithm is not null)
        {
            throw new ConfigBuilderException("Algorithm was already set");
        }

        _algorithm = new SingleStorage();
        _storageAlgorithmType = StorageAlgorithm.SingleStorage;
        return this;
    }

    public ConfigBuilder SetSplitStorage()
    {
        if (_algorithm is not null)
        {
            throw new ConfigBuilderException("Algorithm was already set");
        }

        _algorithm = new SplitStorage();
        _storageAlgorithmType = StorageAlgorithm.SplitStorage;
        return this;
    }

    public ConfigBuilder SetPhysicalFileSystem()
    {
        if (_fileSystem is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _fileSystem = new PhysicalFileSystem();
        _fileSystemType = FileSystemType.PhysicalFileSystem;
        return this;
    }

    public ConfigBuilder SetMemoryFileSystem()
    {
        if (_fileSystem is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _fileSystem = new MemoryFileSystem();
        _fileSystemType = FileSystemType.MemoryFileSystem;
        return this;
    }

    public ConfigBuilder SetZipFileSystem()
    {
        if (_fileSystem is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _fileSystem = new ZipArchiveFileSystem();
        _fileSystemType = FileSystemType.ZipFileSystem;
        return this;
    }

    public ConfigBuilder SetRepositoryPath(string repositoryPath)
    {
        if (_repositoryPath is not null)
        {
            throw new ConfigBuilderException("RepositoryPath was already set");
        }

        if (string.IsNullOrEmpty(repositoryPath))
        {
            throw new ConfigBuilderException($"{nameof(repositoryPath)} was null or empty");
        }

        _repositoryPath = repositoryPath;
        return this;
    }

    public Config GetConfig()
    {
        if (_fileSystem is null)
        {
            throw new ConfigBuilderException("FileSystem wasn't set");
        }

        if (_algorithm is null)
        {
            throw new ConfigBuilderException("StorageAlgorithm wasn't set");
        }

        return new Config(_storageAlgorithmType, _fileSystemType, new Repository(_fileSystem, _repositoryPath), _algorithm);
    }

    public void Dispose()
    {
        _fileSystem.Dispose();
    }
}