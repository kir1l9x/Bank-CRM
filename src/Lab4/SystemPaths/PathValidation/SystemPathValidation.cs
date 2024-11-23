namespace Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

public class SystemPathValidation : IPathValidation
{
    public bool IsValid(string path)
    {
        path = Path.GetFullPath(path);
        return File.Exists(path) || Directory.Exists(path);
    }
}