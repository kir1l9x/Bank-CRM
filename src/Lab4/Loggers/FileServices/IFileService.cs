namespace Itmo.ObjectOrientedProgramming.Lab4.Loggers.FileServices;

public interface IFileService
{
    IReadOnlyList<string> CollectLogsToList();

    void WriteLog(string message);

    void ClearLogs();
}