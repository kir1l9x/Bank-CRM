namespace Itmo.ObjectOrientedProgramming.Lab4.Readers;

public class ConsoleReader : IReader
{
    public string? Read()
    {
        return Console.ReadLine();
    }
}