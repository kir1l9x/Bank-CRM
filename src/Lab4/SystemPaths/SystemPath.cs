using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

public class SystemPath : ISystemPath
{
    public string? Path { get; }

    public IPathValidation PathValidType { get; }

    public SystemPath(ContextTools contextTools, string path, IPathValidation pathValidation)
    {
        if (!pathValidation.IsValid(path))
        {
            contextTools.Writer.Write("Invalid path");
        }

        Path = path;
        PathValidType = pathValidation;
    }

    public bool Equals(ISystemPath? other)
    {
        if (other is null) return false;

        return Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        return obj is SystemPath other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Path?.GetHashCode(StringComparison.Ordinal) ?? 0;
    }
}