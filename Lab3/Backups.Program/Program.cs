using System.Diagnostics;
using System.IO.Compression;
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
        IFileSystem fs = new PhysicalFileSystem();
        Repository res = new Repository(fs, "/mnt/c/test");
        IBackupTask taskSplit = new BackupTask(new SplitStorage(), res, "Backuptask", 0);
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSplit.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSplit.CreateRestorePoint();

        taskSplit.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSplit.CreateRestorePoint();

        IBackupTask taskSingle = new BackupTask(new SingleStorage(), res, "Backuptask2", 1);
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/test.png"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Unti2424tled.jpg"));
        taskSingle.TrackObject(new BackupObject("/mnt/c/test/Untitled.jpg"));
        taskSingle.CreateRestorePoint();

        taskSingle.UntrackObject(new BackupObject("/mnt/c/test/dz"));
        taskSingle.CreateRestorePoint();

        IFileSystem mfs = new MemoryFileSystem();
        string rootPath = "/mnt/test";
        Repository memRep = new Repository(mfs, rootPath);
        mfs.CreateDirectory(rootPath);
        using (var test = mfs.CreateFile(UPath.Combine(rootPath, "kek1.f")))
        {
            test.Write(Encoding.ASCII.GetBytes("KEK1"));
        }

        using (var test1 = mfs.CreateFile(UPath.Combine(rootPath, "kek2.f")))
        {
            test1.Write(Encoding.ASCII.GetBytes("KEK2"));
        }

        using (var test2 = mfs.CreateFile(UPath.Combine(rootPath, "kek3.f")))
        {
            test2.Write(Encoding.ASCII.GetBytes("KEK3"));
        }

        mfs.ReadAllBytes(UPath.Combine(rootPath, "kek1.f"));

        IBackupTask taskMemSplit = new BackupTask(new SplitStorage(), memRep, "Backup", 2);
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek1.f").FullName));
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek2.f").FullName));
        taskMemSplit.TrackObject(new BackupObject(UPath.Combine(rootPath, "kek3.f").FullName));
        taskMemSplit.CreateRestorePoint();

        foreach (var path in mfs.EnumeratePaths(UPath.Combine(rootPath, "Backup/0")))
        {
            Console.WriteLine(path.FullName);
        }

        IFileSystem ffs = new PhysicalFileSystem();
        Repository repo = new Repository(ffs, "/mnt/c/test");
        repo.UnzipZipFile("/mnt/c/test/Backuptask2/0/Backuptask2.zip", "/mnt/c/test/unzip");
    }
}