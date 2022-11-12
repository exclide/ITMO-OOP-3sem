using System.IO.Compression;
using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IRepository
{
    public string RootPath { get; }
    void CreateDirectory(string path);
    Stream CreateFile(string path);
    bool DirectoryExists(string path);
    bool FileExists(string path);
    byte[] ReadAllBytes(string path);
    void DeleteFile(string path);
    Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess);
    void WriteAllBytes(string path, byte[] bytes);
    string PathCombine(string path1, string path2);
    IEnumerable<UPath> EnumeratePaths(string path);
    void CreateEntryInZip(ZipArchive zip, string path, string zipRoot = "");
    void UnzipZipFile(string zipPath, string targetRootPath);
    RepositoryType GetRepositoryType();
}