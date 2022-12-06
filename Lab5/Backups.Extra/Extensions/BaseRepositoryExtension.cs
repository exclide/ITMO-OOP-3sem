using Backups.Entities;
using Backups.Interfaces;

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
            targetRepository.DeleteFile(newZipPath);
        }
    }
}