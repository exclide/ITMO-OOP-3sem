using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SingleStorage : AbstractAlgorithm
{
    public override StorageAlgorithmType GetAlgorithmType() => StorageAlgorithmType.SingleStorage;

    protected override IEnumerable<Storage> RunInternal(
        IRepository repository,
        IEnumerable<BackupObject> backupObjects,
        string restorePointDir,
        string backupTaskName)
    {
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