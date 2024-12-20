namespace ConsoleUI;

public interface IScenario
{
    string Name { get; }

    void Run();
}