namespace Itmo.ObjectOrientedProgramming.Lab4.Loggers.LogLevels;

public abstract record LogLevel
{
    public string LevelName { get; init; }

    public int LevelNumber { get; init; }

    private LogLevel(string levelName, int levelNumber)
    {
        LevelName = levelName;
        LevelNumber = levelNumber;
    }

    public sealed record Info : LogLevel
    {
        public Info() : base("Info", 0) { }
    }

    public sealed record Warning : LogLevel
    {
        public Warning() : base("Warning", 1) { }
    }

    public sealed record Errors : LogLevel
    {
        public Errors() : base("Error", 2) { }
    }

    public sealed record Fatal : LogLevel
    {
        public Fatal() : base("Fatal", 3) { }
    }
}