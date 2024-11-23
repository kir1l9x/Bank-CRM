namespace Itmo.ObjectOrientedProgramming.Lab4.Loggers.FileServices;

public class FileService : IFileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteLog(string message)
    {
        var writer = new StreamWriter(_filePath, true);

        writer.WriteLine(message);
        writer.Close();
    }

    public IReadOnlyList<string> CollectLogsToList()
    {
        return File.ReadAllLines(_filePath);
    }

    public void ClearLogs()
    {
        File.WriteAllText(_filePath, string.Empty);
    }
}