using System.Text;
using Backups.Controllers;
using Backups.Models;
using Backups.StorageAlgorithms;
using Xunit;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupTests
{
    private readonly BackupTasksController _backupController = new BackupTasksController();

    [Fact]
    public void AddBackupTaskCreateRestorePoints_RestorePointsExistStorageExist()
    {
        string rootPath = "/mnt/test";
        Config cfg1 = new ConfigBuilder()
            .SetRepositoryPath(rootPath)
            .SetSplitStorage()
            .SetMemoryFileSystem()
            .GetConfig();
        var backupTask = _backupController.AddBackupTask(cfg1, "BackupTest");
        var backupController = new BackupObjectController(backupTask);

        string filePath1 = "/mnt/test/file1";
        string filePath2 = "/mnt/test/file2";

        using (var testFile1 = cfg1.Repository.CreateFile(filePath1))
        {
            testFile1.Write(Encoding.ASCII.GetBytes("KEK1"));
        }

        using (var testFile2 = cfg1.Repository.CreateFile(filePath2))
        {
            testFile2.Write(Encoding.ASCII.GetBytes("KEK2"));
        }

        var backupObject1 = new BackupObject(filePath1);
        var backupObject2 = new BackupObject(filePath2);

        backupController.TrackBackupObject(backupObject1);
        backupController.TrackBackupObject(backupObject2);

        backupController.CreateRestorePoint();
        Assert.Equal(1, backupController.GetRestorePointCount());
        Assert.Equal(2, backupController.GetStorageCount());

        backupController.UntrackBackupObject(backupObject2);

        backupController.CreateRestorePoint();
        Assert.Equal(2, backupController.GetRestorePointCount());
        Assert.Equal(3, backupController.GetStorageCount());

        Assert.True(cfg1.Repository.DirectoryExists("/mnt/test/BackupTest/0"));
        Assert.True(cfg1.Repository.DirectoryExists("/mnt/test/BackupTest/1"));

        var restorePoint1Storages = backupController.GetStoragesForRestorePointNumber(0).ToList();
        var restorePoint2Storages = backupController.GetStoragesForRestorePointNumber(1).ToList();

        foreach (var storageFile in restorePoint1Storages)
        {
            Assert.True(cfg1.Repository.FileExists(storageFile.Path));
        }

        foreach (var storageFile in restorePoint2Storages)
        {
            Assert.True(cfg1.Repository.FileExists(storageFile.Path));
        }

        cfg1.Repository.DeleteFile(filePath1);
        cfg1.Repository.DeleteFile(filePath2);

        Assert.False(cfg1.Repository.FileExists(filePath1));
        Assert.False(cfg1.Repository.FileExists(filePath2));

        foreach (var storageFile in restorePoint1Storages)
        {
            cfg1.Repository.UnzipZipFile(storageFile.Path, rootPath);
        }

        Assert.True(cfg1.Repository.FileExists(filePath1));
        Assert.True(cfg1.Repository.FileExists(filePath2));

        using (var file1 = cfg1.Repository.OpenFile(filePath1, FileMode.Open, FileAccess.Read))
        {
            using var streamReader = new StreamReader(file1);
            string text = streamReader.ReadLine();
            Assert.Equal("KEK1", text);
        }

        using (var file2 = cfg1.Repository.OpenFile(filePath2, FileMode.Open, FileAccess.Read))
        {
            using var streamReader = new StreamReader(file2);
            string text = streamReader.ReadLine();
            Assert.Equal("KEK2", text);
        }
    }
}