using System.Reflection;
using System.Security.Cryptography;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.StorageAlgorithms;
using Zio;
using Zio.FileSystems;

namespace Backups.Models;

public class ConfigBuilder
{
    private readonly string _repositoryPath;
    private IRepository _repository;
    private IStorageAlgorithm _algorithm;

    public ConfigBuilder(string repositoryPath)
    {
        if (string.IsNullOrEmpty(repositoryPath))
        {
            throw new ConfigBuilderException($"{nameof(repositoryPath)} was null or empty");
        }

        _repositoryPath = repositoryPath;
    }

    public ConfigBuilder SetSingleStorage()
    {
        if (_algorithm is not null)
        {
            throw new ConfigBuilderException("Algorithm was already set");
        }

        _algorithm = new SingleStorage();
        return this;
    }

    public ConfigBuilder SetSplitStorage()
    {
        if (_algorithm is not null)
        {
            throw new ConfigBuilderException("Algorithm was already set");
        }

        _algorithm = new SplitStorage();
        return this;
    }

    public ConfigBuilder SetPhysicalFileSystem()
    {
        if (_repository is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _repository = new PhysicalRepository(_repositoryPath);
        return this;
    }

    public ConfigBuilder SetMemoryFileSystem()
    {
        if (_repository is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _repository = new InMemoryRepository(_repositoryPath);
        return this;
    }

    public ConfigBuilder SetZipFileSystem()
    {
        if (_repository is not null)
        {
            throw new ConfigBuilderException("FileSystem was already set");
        }

        _repository = new ZipRepository(_repositoryPath);
        return this;
    }

    public Config GetConfig()
    {
        if (_repository is null)
        {
            throw new ConfigBuilderException("FileSystem wasn't set");
        }

        if (_algorithm is null)
        {
            throw new ConfigBuilderException("StorageAlgorithm wasn't set");
        }

        return new Config(_repository, _algorithm);
    }
}