using Zio;
using Zio.FileSystems;

namespace Backups.Program;

public class Program
{
    public static void Main(string[] args)
    {
        IFileSystem test = new PhysicalFileSystem();
        PhysicalFileSystem ffs = new PhysicalFileSystem();
        MemoryFileSystem mfs = new MemoryFileSystem();
    }
}