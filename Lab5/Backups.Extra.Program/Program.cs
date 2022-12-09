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
    public static BackupTaskContext BackupContext { get; } = new BackupTaskContext($"C:\\backupTasks");
    public static BackupTasksExtraController BackupController { get; } = new BackupTasksExtraController(BackupContext);
    
    public static void AddTaskAndRestorePoints(ILimitAlgorithm limitAlgorithm)
    {
        string repoPath = "/mnt/c/test";
        Config cfg = new ConfigBuilder(repoPath)
            .SetSingleStorage()
            .SetPhysicalFileSystem()
            .GetConfig();

        string taskName = $"Task{BackupContext.BackupTaskExtras.Count}";

        var back = BackupController.AddBackupTaskExtra(
            cfg,
            new ConsoleLogger(true),
            limitAlgorithm,
            taskName);

        using (var backupObjectController = new BackupObjectController(back, BackupContext))
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
        }
    }

    public static void ReadTasksAddTrackedCreateNewRestorePoints()
    {
        var tasks = BackupContext.BackupTaskExtras;
        foreach (var task in tasks)
        {
            using (var taskController = new BackupObjectController(task, BackupContext))
            {
                var obj1 = new BackupObject("/mnt/c/test/testfile");
                var obj2 = new BackupObject("/mnt/c/test/0.png");
                taskController.TrackObject(obj1);
                taskController.TrackObject(obj2);
                taskController.CreateRestorePoint();
            }
        }
        
    }

    public static void ReadTasksCreateRestorePointsCheckLimitAlgorithms(int numberOfRestorePoints)
    {
        var tasks = BackupContext.BackupTaskExtras;
        foreach (var task in tasks)
        {
            using (var taskController = new BackupObjectController(task, BackupContext))
            {
                for (int i = 0; i < numberOfRestorePoints; i++)
                {
                    taskController.CreateRestorePoint();
                }
            }
        }
    }

    public static void CreateTasks()
    {
        AddTaskAndRestorePoints(new NumberAlgorithm(1));
        AddTaskAndRestorePoints(new DateAlgorithm(DateTime.Now.AddDays(1)));
        AddTaskAndRestorePoints(new NumberAlgorithm(10));
        AddTaskAndRestorePoints(new DateAlgorithm(DateTime.Now.AddDays(-1)));
        AddTaskAndRestorePoints(new HybridAlgorithm(
            new List<ILimitAlgorithm>() {
            new NumberAlgorithm(5), new DateAlgorithm(DateTime.Now.AddMinutes(1)) }, 
            true));
    }

    public static void DeleteAllTasks()
    {
        BackupController.DeleteAllBackupTasks();
    }

    public static void CheckNumberOfRestorePointsForAllTasks()
    {
        var tasks = BackupContext.BackupTaskExtras;
        foreach (var task in tasks)
        {
            Console.WriteLine($"RPs for task {task.BackupTask.TaskName}: " +
                              $"{task.BackupTask.Backup.RestorePoints.Count()}");
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. CreateTasks\n" +
                              "2. ReadTasksAddTrackedCreateNewRestorePoints\n" +
                              "3. ReadTasksCreateRestorePointsCheckLimitAlgorithms\n" +
                              "4. DeleteAllTasks\n" +
                              "5. CheckNumberOfRestorePointsForAllTasks\n" +
                              "Type number");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateTasks();
                    break;
                case "2":
                    ReadTasksAddTrackedCreateNewRestorePoints();
                    break;
                case "3":
                    ReadTasksCreateRestorePointsCheckLimitAlgorithms(20);
                    break;
                case "4":
                    DeleteAllTasks();
                    break;
                case "5":
                    CheckNumberOfRestorePointsForAllTasks();
                    break;
            }
        }
    }
}