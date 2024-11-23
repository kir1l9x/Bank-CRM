using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public interface IFileSystem
{
    void Connect(ISystemPath root);

    void Disconnect();

    void TreeList(int depth);

    void ToGoTo(ISystemPath path);

    void MoveFile(string sourceName, string destName);

    void CopyFile(string sourceName, string destName);

    void DeleteFile(string sourceName);

    void RenameFile(string sourceName, string newFileName);

    void ShowFile(string sourceFileName);
}