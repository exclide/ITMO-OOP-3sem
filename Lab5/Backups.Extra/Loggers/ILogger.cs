using Backups.Extra.Models;

namespace Backups.Extra.Loggers;

public interface ILogger
{
    LoggerType LoggerType { get; }
    void Log(string message);
}