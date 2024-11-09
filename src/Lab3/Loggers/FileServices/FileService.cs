namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers.FileServices;

public class FileService : IFileService
{
    private const string FilePath = @"C:\Users\darli\RiderProjects\kir1l9x\src\Lab3\Loggers\Logs.txt";

    public void WriteLog(string message, Exception? exception = null)
    {
        var writer = new StreamWriter(FilePath, true);

        writer.WriteLine(message);
        writer.Close();
    }

    public IReadOnlyList<string> CollectLogsToList()
    {
        return File.ReadAllLines(FilePath);
    }

    public void ClearLogs()
    {
        File.WriteAllText(FilePath, string.Empty);
    }
}