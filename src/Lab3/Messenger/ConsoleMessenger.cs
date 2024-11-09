namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger;

public class ConsoleMessenger : IMessenger
{
    public void ShowMessage(string message)
    {
        string result = $"messenger: {message}";
        Console.WriteLine(result);
    }
}