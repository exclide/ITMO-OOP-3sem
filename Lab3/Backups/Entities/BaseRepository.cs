using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public abstract class BaseRepository : IRepository
{
    private readonly IFileSystem _fileSystem;

    protected BaseRepository(IFileSystem fileSystem, string rootPath)
    {
        ArgumentNullException.ThrowIfNull(fileSystem);
        if (string.IsNullOrEmpty(rootPath))
        {
            throw new BackupException($"{nameof(rootPath)} was null or empty");
        }

        _fileSystem = fileSystem;
        RootPath = rootPath;

        if (!_fileSystem.DirectoryExists(rootPath))
        {
            _fileSystem.CreateDirectory(rootPath);
        }
    }

    public string RootPath { get; }
    public IFileSystem FileSystem => _fileSystem;

    public void CreateDirectory(string path) => _fileSystem.CreateDirectory(path);
    public Stream CreateFile(string path) => _fileSystem.CreateFile(path);
    public bool DirectoryExists(string path) => _fileSystem.DirectoryExists(path);
    public bool FileExists(string path) => _fileSystem.FileExists(path);
    public byte[] ReadAllBytes(string path) => _fileSystem.ReadAllBytes(path);
    public void DeleteFile(string path) => _fileSystem.DeleteFile(path);
    public Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess) =>
        _fileSystem.OpenFile(path, fileMode, fileAccess);
    public void WriteAllBytes(string path, byte[] bytes) => _fileSystem.WriteAllBytes(path, bytes);
    public string PathCombine(string path1, string path2) => UPath.Combine(path1, path2).FullName;
    public IEnumerable<UPath> EnumeratePaths(string path) => _fileSystem.EnumeratePaths(path);
    public void DeleteDirectory(string path, bool recursive) => _fileSystem.DeleteDirectory(path, recursive);

    public void CreateEntryInZip(ZipArchive zip, string path, string zipRoot = "")
    {
        ArgumentNullException.ThrowIfNull(zip);
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(zipRoot);

        if (DirectoryExists(path))
        {
            string folderPath = $"{zipRoot}{Path.GetFileName(path)}/";
            zip.CreateEntry(folderPath);
            foreach (var upath in _fileSystem.EnumeratePaths(path))
            {
                CreateEntryInZip(zip, upath.FullName, folderPath);
            }
        }
        else
        {
            string filePath = $"{zipRoot}{Path.GetFileName(path)}";
            var file = zip.CreateEntry(filePath);
            using var entryStream = file.Open();
            Console.WriteLine(file.FullName);
            entryStream.Write(ReadAllBytes(path));
        }
    }

    public void UnzipZipFile(string zipPath, string targetRootPath)
    {
        ArgumentNullException.ThrowIfNull(zipPath);
        ArgumentNullException.ThrowIfNull(targetRootPath);

        using (var memStream = new MemoryStream())
        {
            memStream.Write(ReadAllBytes(zipPath));
            memStream.Seek(0, SeekOrigin.Begin);

            using (var zip = new ZipArchive(memStream, ZipArchiveMode.Read, true))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (entry.FullName.EndsWith("/"))
                    {
                        string folderName = $"{targetRootPath}/{entry.FullName}";
                        CreateDirectory(folderName);
                    }
                    else
                    {
                        using var stream = entry.Open();
                        using var ms = new MemoryStream();
                        stream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        string fileName = $"{targetRootPath}/{entry.FullName}";
                        WriteAllBytes(fileName, bytes);
                    }
                }
            }
        }
    }

    public override string ToString()
    {
        return $"RepositoryType: {GetRepositoryType()}, {nameof(RootPath)}: {RootPath}";
    }

    public abstract RepositoryType GetRepositoryType();
}