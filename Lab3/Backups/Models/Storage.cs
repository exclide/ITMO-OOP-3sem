namespace Backups.Models;

public class Storage
{
    public Storage(string path)
    {
        Path = path;
    }

    public string Path { get; }
}