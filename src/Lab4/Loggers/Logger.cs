using Itmo.ObjectOrientedProgramming.Lab4.Loggers.FileServices;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers.LogLevels;

namespace Itmo.ObjectOrientedProgramming.Lab4.Loggers;

public class Logger(IFileService fileService) : ILogger
{
    private readonly string _currentTime = $"{DateTime.Now.ToLongDateString()} at {DateTime.Now.ToLongTimeString()}";

    public void Log(string message, LogLevel level)
    {
        string logMessage = CreateLogMessage(message, level);

        switch (level)
        {
            case LogLevel.Info:
                LogInfo(logMessage);
                break;
            case LogLevel.Warning:
                LogWarning(logMessage);
                break;
            case LogLevel.Errors:
                LogError(logMessage);
                break;
            case LogLevel.Fatal:
                LogFatal(logMessage);
                break;
        }
    }

    public void LogInfo(string message)
    {
        fileService.WriteLog(message);
    }

    public void LogWarning(string message)
    {
        fileService.WriteLog(message);
    }

    public void LogError(string message)
    {
        fileService.WriteLog(message);
    }

    public void LogFatal(string message)
    {
        fileService.WriteLog(message);
    }

    public void ClearLogs()
    {
        fileService.ClearLogs();
    }

    public IReadOnlyList<string> LogsList()
    {
        return fileService.CollectLogsToList();
    }

    private string CreateLogMessage(string message, LogLevel level)
    {
        string logMessage = $"{_currentTime}: {level.ToString()} | {message}";

        return logMessage;
    }
}