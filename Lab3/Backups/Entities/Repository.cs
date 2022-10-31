using Zio;

namespace Backups.Entities;

public class Repository
{
    private readonly IFileSystem _fileSystem;
    private readonly string _rootPath;

    public Repository(IFileSystem fileSystem, string rootPath)
    {
        _fileSystem = fileSystem;
        _rootPath = rootPath;

        if (!_fileSystem.DirectoryExists(rootPath))
        {
            _fileSystem.CreateDirectory(rootPath);
        }
    }
}