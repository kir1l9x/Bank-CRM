using Itmo.ObjectOrientedProgramming.Lab3.Loggers.FileServices;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers.LogLevels;

namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class Logger(IFileService fileService) : ILogger
{
    private readonly string _currentTime = $"{DateTime.Now.ToLongDateString()} at {DateTime.Now.ToLongTimeString()}";

    public void Log(string message, LogLevel level, Exception? exception = null)
    {
        string logMessage = CreateLogMessage(message, level, exception);

        switch (level)
        {
            case LogLevel.Info:
                LogInfo(logMessage);
                break;
            case LogLevel.Warning:
                LogWarning(logMessage);
                break;
            case LogLevel.Error:
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

    private string CreateLogMessage(string message, LogLevel level, Exception? exception)
    {
        string logMessage = $"{_currentTime}: {level.ToString()} | {message}";
        if (exception != null)
        {
            logMessage = $"{logMessage} | {exception.Message}";
        }

        return logMessage;
    }
}