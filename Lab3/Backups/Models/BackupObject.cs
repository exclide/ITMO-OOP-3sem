namespace Backups.Models;

public class BackupObject : IEquatable<BackupObject>
{
    public BackupObject(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public override int GetHashCode() => Path.GetHashCode();

    public override bool Equals(object obj) => this.Equals(obj as BackupObject);
    public bool Equals(BackupObject other) => other?.Path.Equals(Path) ?? false;
}