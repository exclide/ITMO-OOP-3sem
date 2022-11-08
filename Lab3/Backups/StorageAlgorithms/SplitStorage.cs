using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.StorageAlgorithms;

public class SplitStorage : AbstractAlgorithm
{
    public override StorageAlgorithmType GetAlgorithmType() => StorageAlgorithmType.SingleStorage;

    protected override IEnumerable<Storage> RunInternal(
        IRepository repository,
        IEnumerable<BackupObject> backupObjects,
        string restorePointDir,
        string backupTaskName)
    {
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