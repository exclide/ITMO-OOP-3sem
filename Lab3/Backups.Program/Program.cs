using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Channels;
using Backups.Entities;
using Backups.Models;
using Backups.StorageAlgorithms;
using Zio;
using Zio.FileSystems;

namespace Backups.Program;

public class Program
{
    public static void Main(string[] args)
    {
        IFileSystem fs = new PhysicalFileSystem();
        Repository res = new Repository(fs, "/mnt/c/test");
        BackupTask taskSplit = new BackupTask(new SplitStorage(), res, "Backuptask");
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSplit.CreateRestorePoint();

        taskSplit.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.CreateRestorePoint();

        BackupTask taskSingle = new BackupTask(new SingleStorage(), res, "Backuptask2");
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSingle.CreateRestorePoint();

        taskSingle.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.CreateRestorePoint();
    }
}