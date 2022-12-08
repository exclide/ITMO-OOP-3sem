using Backups.Entities;
using Backups.Extra.Contexts;
using Backups.Extra.Controllers;
using Backups.Extra.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Loggers;
using Backups.Models;
using Backups.StorageAlgorithms;
using Newtonsoft.Json;

namespace Backups.Extra.Program;

public class Program
{
    public static void Main(string[] args)
    {
        var backupContext =  new BackupTaskContext($"C:\\backupTasks");
        var task = backupContext.BackupTaskExtras.First();
        Console.WriteLine(task.BackupTask.Backup.RestorePoints.Count());
        task.RestoreRestorePoint(task.BackupTask.Backup.RestorePoints.First());
        /*
        var backupController = new BackupTasksExtraController(backupContext);
        
        string repoPath = "/mnt/c/test";
        Config cfg = new ConfigBuilder(repoPath)
            .SetSingleStorage()
            .SetPhysicalFileSystem()
            .GetConfig();

        var back = backupController.AddBackupTaskExtra(
            cfg,
            new ConsoleLogger(true),
            new NumberAlgorithm(5),
            "TestTaskk");

        using (var backupObjectController = new BackupObjectController(back, backupContext))
        {
            var obj1 = new BackupObject("/mnt/c/test/test.png");
            var obj2 = new BackupObject("/mnt/c/test/Untitled.jpg");
            var obj3 = new BackupObject("/mnt/c/test/Unti2424tled.jpg");
            backupObjectController.TrackObject(obj1);
            backupObjectController.TrackObject(obj2);
            backupObjectController.TrackObject(obj3);

            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
            backupObjectController.CreateRestorePoint();
        }*/
    }
}