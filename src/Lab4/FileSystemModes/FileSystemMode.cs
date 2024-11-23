namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemModes;

public abstract record FileSystemMode
{
    public sealed record Local : FileSystemMode;
}