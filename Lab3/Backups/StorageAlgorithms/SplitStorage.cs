using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SplitStorage : IStorageAlgorithm
{
    public IEnumerable<Storage> Run(
        Repository repository,
        IEnumerable<BackupObject> backupObjects,
        int restorePointNumber,
        string backupTaskName)
    {
        string backupTaskDirectory = repository.PathCombine(repository.RootPath, backupTaskName);

        if (!repository.DirectoryExists(backupTaskDirectory))
        {
            repository.CreateDirectory(backupTaskDirectory);
        }

        string restorePointDir = repository.PathCombine(backupTaskDirectory, $"{restorePointNumber}");
        repository.CreateDirectory(restorePointDir);

        var storages = new List<Storage>();

        foreach (var backupObject in backupObjects)
        {
            string storageDir = repository.PathCombine(restorePointDir, $"{Path.GetFileName(backupObject.Path)}.zip");
            storages.Add(new Storage(storageDir));

            using (var memStream = new MemoryStream())
            {
                using (var zip = new ZipArchive(memStream, ZipArchiveMode.Create, true))
                {
                    repository.CreateEntryInZip(zip, backupObject.Path);
                }

                byte[] arr = memStream.ToArray();
                repository.WriteAllBytes(storageDir, arr);
            }
        }

        return storages;
    }
}