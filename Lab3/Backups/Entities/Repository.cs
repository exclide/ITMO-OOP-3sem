using System.IO.Compression;
using Zio;

namespace Backups.Entities;

public class Repository
{
    private readonly IFileSystem _fileSystem;

    public Repository(IFileSystem fileSystem, string rootPath)
    {
        _fileSystem = fileSystem;
        RootPath = rootPath;

        if (!_fileSystem.DirectoryExists(rootPath))
        {
            _fileSystem.CreateDirectory(rootPath);
        }
    }

    public string RootPath { get; }

    public void CreateDirectory(string path) => _fileSystem.CreateDirectory(path);

    public void CreateFile(string path) => _fileSystem.CreateFile(path);

    public bool DirectoryExists(string path) => _fileSystem.DirectoryExists(path);

    public string PathCombine(string path1, string path2) => UPath.Combine(path1, path2).FullName;

    public void CreateEntryInZip(ZipArchive zip, string path, string zipRoot = "")
    {
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
                        CreateDirectory($"{targetRootPath}/{entry.FullName}");
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

    public byte[] ReadAllBytes(string path) => _fileSystem.ReadAllBytes(path);
    public void WriteAllBytes(string path, byte[] bytes) => _fileSystem.WriteAllBytes(path, bytes);
}