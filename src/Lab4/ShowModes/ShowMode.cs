namespace Itmo.ObjectOrientedProgramming.Lab4.ShowModes;

public abstract record ShowMode
{
    public sealed record ConsoleMode : ShowMode;
}