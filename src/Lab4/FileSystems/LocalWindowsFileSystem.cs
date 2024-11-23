using Itmo.ObjectOrientedProgramming.Lab4.Loggers;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers.FileServices;
using Itmo.ObjectOrientedProgramming.Lab4.Loggers.LogLevels;
using Itmo.ObjectOrientedProgramming.Lab4.Readers;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.TreeSignSettings;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class LocalWindowsFileSystem : IFileSystem
{
    public ContextTools Tools { get; }

    public ISystemPath? Root { get; private set; }

    public ISystemPath? GoRoot { get; private set; }

    public TreeSignSetting TreeSigns { get; private set; } = new TreeSignSetting();

    public LocalWindowsFileSystem(ISystemPath root, string logFilePath)
    {
        Root = root;
        GoRoot = root;

        Tools = new ContextTools(new ConsoleReader(), new ConsoleWriter(), new Logger(new FileService(logFilePath)));
    }

    public void Connect(ISystemPath root)
    {
        Root = root;
    }

    public void Disconnect()
    {
        Root = null;
        GoRoot = null;
    }

    public void ToGoTo(ISystemPath path)
    {
        GoRoot = path;
    }

    public void MoveFile(string sourceName, string destName)
    {
        Directory.Move(sourceName, destName);
    }

    public void CopyFile(string sourceName, string destName)
    {
        var dir = new DirectoryInfo(sourceName);

        DirectoryInfo[] dirs = dir.GetDirectories();
        Directory.CreateDirectory(destName);
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destName, file.Name);
            file.CopyTo(targetFilePath);
        }

        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destName, subDir.Name);
            CopyFile(subDir.FullName, newDestinationDir);
        }

        Tools.Logger.Log($"Copied {sourceName} to {destName}", new LogLevel.Info());
    }

    public void DeleteFile(string sourceName)
    {
        Directory.Delete(sourceName, true);

        Tools.Logger.Log($"Deleted {sourceName}", new LogLevel.Info());
    }

    public void RenameFile(string sourceName, string newFileName)
    {
        var file = new FileInfo(sourceName);
        file.MoveTo(Path.Combine(sourceName, newFileName));
        Tools.Logger.Log($"Renamed {sourceName} to {newFileName}", new LogLevel.Info());
    }

    public void ShowFile(string sourceFileName)
    {
        Tools.Writer.Write(File.ReadAllText(sourceFileName));
    }

    public void TreeList(int depth)
    {
        var sb = new StringBuilder();

        RenderDirectory(path: Root?.Path ?? throw new InvalidOperationException(), sb, TreeSigns, 0);

        Tools.Writer.Write(sb.ToString());
    }

    private void RenderDirectory(string path, StringBuilder sb, TreeSignSetting settings, int depth)
    {
        if (!Directory.Exists(path))
        {
            sb.AppendLine($"Path '{path}' does not exist.");
            return;
        }

        sb.AppendLine($"{new string(' ', depth * settings.Indent.Length)}{settings.BranchSymbol}{settings.DirectorySymbol} {Path.GetFileName(path)}");

        foreach (string file in Directory.GetFiles(path))
        {
            sb.AppendLine($"{new string(' ', (depth + 1) * settings.Indent.Length)}{settings.BranchSymbol}{settings.FileSymbol} {Path.GetFileName(file)}");
        }

        foreach (string directory in Directory.GetDirectories(path))
        {
            RenderDirectory(directory, sb, settings, depth + 1);
        }
    }
}