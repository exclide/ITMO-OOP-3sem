using Backups.Models;
using Zio.FileSystems;

namespace Backups.Entities;

public class ZipRepository : BaseRepository
{
    public ZipRepository(string rootPath)
        : base(new ZipArchiveFileSystem(), rootPath)
    {
    }

    public override RepositoryType GetRepositoryType() => RepositoryType.ZipRepository;
}