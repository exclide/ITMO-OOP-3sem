using System.Text;
using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Loggers;
using Backups.Models;
using Xunit;

namespace Backups.Extra.Test;

public class BackupTaskExtraTests
{
    [Fact]
    public void AddRestorePointsCheckLimitAlgorithm()
    {
        string repoPath = "/mnt/test";
        Config cfg = new ConfigBuilder(repoPath)
            .SetSplitStorage()
            .SetMemoryFileSystem()
            .GetConfig();

        var backupTask = new BackupTaskExtra(
            new BackupTask(cfg, "Test", 0),
            new ConsoleLogger(true),
            new NumberAlgorithm(3));

        string filePath1 = "/mnt/test/file1";
        string filePath2 = "/mnt/test/file2";
        string filePath3 = "/mnt/test/file3";

        using (var testFile1 = cfg.Repository.CreateFile(filePath1))
        {
            testFile1.Write(Encoding.ASCII.GetBytes("KEK1"));
        }

        using (var testFile2 = cfg.Repository.CreateFile(filePath2))
        {
            testFile2.Write(Encoding.ASCII.GetBytes("KEK2"));
        }

        using (var testFile3 = cfg.Repository.CreateFile(filePath3))
        {
            testFile3.Write(Encoding.ASCII.GetBytes("KEK3"));
        }

        var backupObject1 = new BackupObject(filePath1);
        var backupObject2 = new BackupObject(filePath2);
        var backupObject3 = new BackupObject(filePath3);

        backupTask.TrackObject(backupObject1);
        backupTask.CreateRestorePoint();
        backupTask.TrackObject(backupObject2);
        backupTask.CreateRestorePoint();
        backupTask.TrackObject(backupObject3);
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();
        backupTask.CreateRestorePoint();

        Assert.Equal(4, backupTask.BackupTask.Backup.RestorePoints.Count());
        Assert.Equal(3, backupTask.BackupTask.Backup.RestorePoints.First().BackupObjects.Count);
        Assert.Equal(3, backupTask.BackupTask.Backup.RestorePoints.First().Storages.Count);
    }
}