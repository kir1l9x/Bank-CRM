namespace Itmo.ObjectOrientedProgramming.Lab3.Displays.Drivers;

public class FileDriver(string filePath) : IDriver
{
    public void CleanUp()
    {
        File.WriteAllText(filePath, string.Empty);
    }

    public void ShowText(string text)
    {
        File.WriteAllText(filePath, text);
    }
}