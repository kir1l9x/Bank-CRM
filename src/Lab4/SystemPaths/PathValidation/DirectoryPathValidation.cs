namespace Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

public class DirectoryPathValidation : IPathValidation
{
    public bool IsValid(string path)
    {
        path = Path.GetFullPath(path);
        return Directory.Exists(path);
    }
}