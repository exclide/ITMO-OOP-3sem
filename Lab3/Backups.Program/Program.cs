using System.Collections;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Backups.StorageAlgorithms;
using Zio;
using Zio.FileSystems;

namespace Backups.Program;

public class Program
{
    public static void Main(string[] args)
    {
        Config cfg1 = new ConfigBuilder("/mnt/c/test")
            .SetSplitStorage()
            .SetPhysicalFileSystem()
            .GetConfig();

        IBackupTask taskSplit = new BackupTask(cfg1, "Backuptask", 0);
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSplit.CreateRestorePoint();

        taskSplit.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.CreateRestorePoint();

        Config cfg2 = new ConfigBuilder("/mnt/c/test")
            .SetSingleStorage()
            .SetPhysicalFileSystem()
            .GetConfig();

        IBackupTask taskSingle = new BackupTask(cfg2, "Backuptask2", 1);
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSingle.CreateRestorePoint();

        taskSingle.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.CreateRestorePoint();

        string rootPath = "/mnt/test";
        Config cfg3 = new ConfigBuilder(rootPath)
            .SetSplitStorage()
            .SetMemoryFileSystem()
            .GetConfig();

        cfg3.Repository.CreateDirectory(rootPath);
        using (var test = cfg3.Repository.CreateFile(UPath.Combine(rootPath, "kek1.f").FullName))
        {
            test.Write(Encoding.ASCII.GetBytes("KEK1"));
        }

        using (var test1 = cfg3.Repository.CreateFile(UPath.Combine(rootPath, "kek2.f").FullName))
        {
            test1.Write(Encoding.ASCII.GetBytes("KEK2"));
        }

        using (var test2 = cfg3.Repository.CreateFile(UPath.Combine(rootPath, "kek3.f").FullName))
        {
            test2.Write(Encoding.ASCII.GetBytes("KEK3"));
        }

        cfg3.Repository.ReadAllBytes(UPath.Combine(rootPath, "kek1.f").FullName);

        IBackupTask taskMemSplit = new BackupTask(cfg3, "Backup", 2);
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek1.f").FullName));
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek2.f").FullName));
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek3.f").FullName));
        taskMemSplit.CreateRestorePoint();

        foreach (var path in cfg3.Repository.EnumeratePaths(UPath.Combine(rootPath, "Backup/0").FullName))
        {
            Console.WriteLine(path.FullName);
        }

        IRepository repo = new PhysicalRepository("/mnt/c/test");
        repo.UnzipZipFile("/mnt/c/test/Backuptask2/0/Backuptask2.zip", "/mnt/c/test/unzip");
    }
}