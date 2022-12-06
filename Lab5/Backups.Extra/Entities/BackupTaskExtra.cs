using Backups.Controllers;
using Backups.Entities;
using Backups.Extra.Extensions;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Loggers;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : IBackupTask
{
    private BackupTask _backupTask;

    public BackupTaskExtra(Config config, ILogger logger, ILimitAlgorithm limitAlgorithm, string taskName, int id)
    {
        _backupTask = new BackupTask(config, taskName, id);
        Logger = logger;
        LimitAlgorithm = limitAlgorithm;
    }

    public ILogger Logger { get; }
    public ILimitAlgorithm LimitAlgorithm { get; }

    public void CreateRestorePoint()
    {
        Logger.Log($"Creating restore point number {_backupTask.Backup.RestorePoints.Count()} " +
                   $"with storage algorithm: {_backupTask.Config.StorageAlgorithmType} and repository " +
                   $"{_backupTask.Config.RepositoryType}");
        _backupTask.CreateRestorePoint();
        ApplyLimits();
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        Logger.Log($"Deleting restore point number {restorePoint.RestorePointNumber}");
        _backupTask.DeleteRestorePoint(restorePoint);
    }

    public void TrackObject(BackupObject backupObject)
    {
        Logger.Log($"Now tracking object {backupObject}");
        _backupTask.TrackObject(backupObject);
    }

    public void UntrackObject(BackupObject backupObject)
    {
        Logger.Log($"No longer tracking object {backupObject}");
        _backupTask.UntrackObject(backupObject);
    }

    public void ApplyLimits()
    {
        Logger.Log($"Applying limit algorithm {LimitAlgorithm.LimitAlgorithmType} for {_backupTask.Backup}");
        var restorePointsToMerge = LimitAlgorithm.Run(_backupTask.Backup.RestorePoints);
        MergeRestorePoints(restorePointsToMerge);
    }

    public void MergeRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);
        var listOfRestorePoints = restorePoints.ToList();

        if (listOfRestorePoints.Count < 2)
        {
            return;
        }

        Logger.Log($"Merging {listOfRestorePoints.Count} restore points");

        string postFix = "tmp";
        string targetLocation = $"{_backupTask.Config.Repository.RootPath}/{postFix}";
        foreach (var point in listOfRestorePoints)
        {
            RestoreRestorePoint(point, postFix);
        }

        var backupObjects = new List<BackupObject>();
        foreach (var path in _backupTask.Config.Repository.EnumeratePaths(targetLocation))
        {
            backupObjects.Add(new BackupObject(path.FullName));
        }

        var lastRestorePoint = listOfRestorePoints.Last();

        var storages = _backupTask.Config.Algorithm.RunAlgo(
            _backupTask.Config.Repository,
            backupObjects,
            lastRestorePoint.RestorePointNumber,
            _backupTask.TaskName).ToList();

        _backupTask.Config.Repository.DeleteDirectory(targetLocation, true);

        var restorePoint = new RestorePoint(
                backupObjects,
                storages,
                listOfRestorePoints.Last().TimeCreated,
                _backupTask.Backup.RestorePoints.Count());

        foreach (var point in listOfRestorePoints)
        {
            if (!point.Equals(lastRestorePoint))
            {
                _backupTask.Backup.DeleteRestorePoint(point);
            }
        }

        lastRestorePoint.BackupObjects = backupObjects;
        lastRestorePoint.Storages = storages;
    }

    public void RestoreRestorePoint(RestorePoint restorePoint, string postFix = "")
    {
        Logger.Log($"Restore restore point number {restorePoint.RestorePointNumber} to " +
                   $"original repository {_backupTask.Config.Repository.GetRepositoryType()}");

        string targetLocation = $"{_backupTask.Config.Repository.RootPath}/{postFix}";

        foreach (var zip in restorePoint.Storages)
        {
            _backupTask.Config.Repository.UnzipZipFile(zip.Path, targetLocation);
        }
    }

    public void RestoreRestorePoint(RestorePoint restorePoint, IRepository repository)
    {
        Logger.Log($"Restore restore point number {restorePoint.RestorePointNumber} to " +
                   $"repository {repository.GetRepositoryType()} at {repository.RootPath}");

        var zipFiles = restorePoint.Storages.Select(s => s.Path);

        _backupTask.Config.Repository.UnzipZipFilesToRepository(zipFiles, repository);
    }
}