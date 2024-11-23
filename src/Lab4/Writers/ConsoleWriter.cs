namespace Itmo.ObjectOrientedProgramming.Lab4.Writers;

public class ConsoleWriter : IWriter
{
    public void Write(string text)
    {
        Console.WriteLine(text);
    }

    public void Clean()
    {
        Console.Clear();
    }
}