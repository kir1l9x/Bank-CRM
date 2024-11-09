using Itmo.ObjectOrientedProgramming.Lab3.Loggers.LogLevels;

namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public interface ILogger
{
    void Log(string message, LogLevel level, Exception? exception = null);

    void LogInfo(string message);

    void LogWarning(string message);

    void LogError(string message);

    void LogFatal(string message);
}