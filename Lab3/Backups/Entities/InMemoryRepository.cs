using Backups.Models;
using Zio.FileSystems;

namespace Backups.Entities;

public class InMemoryRepository : BaseRepository
{
    public InMemoryRepository(string rootPath)
        : base(new MemoryFileSystem(), rootPath)
    {
    }

    public override RepositoryType GetRepositoryType() => RepositoryType.MemoryRepository;
}