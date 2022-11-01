using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SingleStorage : IStorageAlgorithm
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

        string storageDir = repository.PathCombine(restorePointDir, $"{backupTaskName}.zip");
        storages.Add(new Storage(storageDir));

        using (var memStream = new MemoryStream())
        {
            using (var zip = new ZipArchive(memStream, ZipArchiveMode.Create, true))
            {
                foreach (var backupObject in backupObjects)
                {
                    repository.CreateEntryInZip(zip, backupObject.Path);
                }
            }

            byte[] arr = memStream.ToArray();
            repository.WriteAllBytes(storageDir, arr);
        }

        return storages;
    }
}