using Backups.Entities;
using Backups.Interfaces;
using Zio;

namespace Backups.Extra.Extensions;

public static class BaseRepositoryExtension
{
    public static void UnzipZipFilesToRepository(
        this IRepository baseRepository,
        IEnumerable<string> files,
        IRepository targetRepository)
    {
        ArgumentNullException.ThrowIfNull(files);
        ArgumentNullException.ThrowIfNull(targetRepository);

        string targetPath = targetRepository.RootPath;
        string tempZipFolder = $"{targetPath}/temp";
        if (!targetRepository.DirectoryExists(tempZipFolder))
        {
            targetRepository.CreateDirectory(tempZipFolder);
        }

        var newZipPaths = new List<string>();

        foreach (string zipPath in files)
        {
            byte[] zipBytes = baseRepository.ReadAllBytes(zipPath);
            string newZipPath = $"{tempZipFolder}/{Path.GetFileName(zipPath)}";
            newZipPaths.Add(newZipPath);
            targetRepository.WriteAllBytes(newZipPath, zipBytes);
        }

        foreach (string newZipPath in newZipPaths)
        {
            targetRepository.UnzipZipFile(newZipPath, targetPath);
        }

        targetRepository.DeleteDirectory(tempZipFolder, true);
    }

    public static void UnzipZipFilesCrossRepository(
        this IRepository baseRepository,
        IEnumerable<string> files,
        IRepository targetRepository)
    {
        string tmpFolder = $"{baseRepository.RootPath}/tmpFolder";
        foreach (var zipFile in files)
        {
            baseRepository.UnzipZipFile(zipFile, tmpFolder);
        }

        foreach (var file in baseRepository.EnumeratePaths(tmpFolder))
        {
            baseRepository.CopyFileCrossRepository(file.FullName, targetRepository, tmpFolder, true);
        }

        baseRepository.DeleteDirectory(tmpFolder, true);
    }

    public static void CopyFileCrossRepository(
        this IRepository baseRepository,
        string srcPath,
        IRepository targetRepository,
        string targetPath,
        bool overwrite)
    {
        baseRepository.FileSystem.CopyFileCross(srcPath, targetRepository.FileSystem, targetPath, overwrite);
    }
}