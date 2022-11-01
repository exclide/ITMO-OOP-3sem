using System.Text;
using Backups.Controllers;
using Backups.Models;
using Backups.StorageAlgorithms;
using Xunit;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupTests
{
    private BackupController _backupController = new BackupController();

    [Fact]
    public void AddBackupTaskCreateRestorePoints_RestorePointsExistStorageExist()
    {
        string rootPath = "/mnt/test";
        var memRepo = _backupController.CreateRepository(new MemoryFileSystem(), rootPath);
        var backupTask = _backupController.AddBackupTask(new SplitStorage(), memRepo, "BackupTest");

        string filePath1 = "/mnt/test/file1";
        string filePath2 = "/mnt/test/file2";

        using (var testFile1 = memRepo.CreateFile(filePath1))
        {
            testFile1.Write(Encoding.ASCII.GetBytes("KEK1"));
        }

        using (var testFile2 = memRepo.CreateFile(filePath2))
        {
            testFile2.Write(Encoding.ASCII.GetBytes("KEK2"));
        }

        var backupObject1 = new BackupObject(filePath1);
        var backupObject2 = new BackupObject(filePath2);

        _backupController.AddBackupObjectForBackupTask(backupTask, backupObject1);
        _backupController.AddBackupObjectForBackupTask(backupTask, backupObject2);

        var restorePoint1 = _backupController.AddRestorePointForBackupTask(backupTask);
        Assert.Equal(2, restorePoint1.BackupObjects.Count());
        Assert.Equal(2, restorePoint1.Storages.Count());
        Assert.Equal(0, restorePoint1.RestorePointNumber);

        _backupController.RemoveBackupObjectForBackupTask(backupTask, backupObject2);

        var restorePoint2 = _backupController.AddRestorePointForBackupTask(backupTask);
        Assert.Single(restorePoint2.BackupObjects);
        Assert.Single(restorePoint2.Storages);
        Assert.Equal(1, restorePoint2.RestorePointNumber);

        Assert.True(memRepo.DirectoryExists("/mnt/test/BackupTest/0"));
        Assert.True(memRepo.DirectoryExists("/mnt/test/BackupTest/1"));

        foreach (var storageFile in restorePoint1.Storages)
        {
            Assert.True(memRepo.FileExists(storageFile.Path));
        }

        foreach (var storageFile in restorePoint2.Storages)
        {
            Assert.True(memRepo.FileExists(storageFile.Path));
        }

        memRepo.DeleteFile(filePath1);
        memRepo.DeleteFile(filePath2);

        Assert.False(memRepo.FileExists(filePath1));
        Assert.False(memRepo.FileExists(filePath2));

        foreach (var storageFile in restorePoint1.Storages)
        {
            memRepo.UnzipZipFile(storageFile.Path, rootPath);
        }

        Assert.True(memRepo.FileExists(filePath1));
        Assert.True(memRepo.FileExists(filePath2));

        using (var file1 = memRepo.OpenFile(filePath1, FileMode.Open, FileAccess.Read))
        {
            using var streamReader = new StreamReader(file1);
            string text = streamReader.ReadLine();
            Assert.Equal("KEK1", text);
        }

        using (var file2 = memRepo.OpenFile(filePath2, FileMode.Open, FileAccess.Read))
        {
            using var streamReader = new StreamReader(file2);
            string text = streamReader.ReadLine();
            Assert.Equal("KEK2", text);
        }
    }
}