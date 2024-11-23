using Itmo.ObjectOrientedProgramming.Lab4.Loggers.LogLevels;

namespace Itmo.ObjectOrientedProgramming.Lab4.Loggers;

public interface ILogger
{
    void Log(string message, LogLevel level);

    void LogInfo(string message);

    void LogWarning(string message);

    void LogError(string message);

    void LogFatal(string message);
}