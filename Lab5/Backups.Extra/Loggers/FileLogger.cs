using Backups.Exceptions;

namespace Backups.Extra.Loggers;

public class FileLogger : ILogger
{
    private bool _prefixEnabled;
    private string _logFilePath;

    public FileLogger(string logFilePath, bool prefixEnabled)
    {
        if (string.IsNullOrEmpty(logFilePath))
        {
            throw new BackupException($"{nameof(logFilePath)} was null or empty");
        }

        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath);
        }

        _prefixEnabled = prefixEnabled;
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        if (_prefixEnabled)
        {
            message = $"{DateTime.Now} : {message}";
        }

        using (var logWriter = new StreamWriter(_logFilePath, true))
        {
            logWriter.WriteLine(message);
        }
    }
}