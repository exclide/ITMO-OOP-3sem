using Backups.Models;
using Zio.FileSystems;

namespace Backups.Entities;

public class PhysicalRepository : BaseRepository
{
    public PhysicalRepository(string rootPath)
        : base(new PhysicalFileSystem(), rootPath)
    {
    }

    public override RepositoryType GetRepositoryType() => RepositoryType.PhysicalRepository;
}