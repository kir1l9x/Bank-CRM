using Itmo.ObjectOrientedProgramming.Lab4.ShowModes;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class FileShowCommand : ICommand
{
    private readonly ISystemPath _filePath;

    private FileShowCommand(ISystemPath filePath, ShowMode showMode)
    {
        _filePath = filePath;
    }

    public static FileShowCommandBuilder Builder()
    {
        return new FileShowCommandBuilder();
    }

    public class FileShowCommandBuilder
    {
        private ISystemPath? _filePath;
        private ShowMode? _showMode;

        public FileShowCommandBuilder AddFilePath(ISystemPath filePath)
        {
            _filePath = filePath;
            return this;
        }

        public FileShowCommandBuilder AddShowMode(ShowMode mode)
        {
            _showMode = mode;
            return this;
        }

        public ICommand Build()
        {
            return new FileShowCommand(
                _filePath ?? throw new ArgumentNullException(nameof(_filePath)),
                _showMode ?? new ShowMode.ConsoleMode());
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.ShowFile(_filePath.Path ?? throw new Exception());
    }

    public bool Equals(FileShowCommand? other)
    {
        if (other == null) return false;

        return Equals(_filePath, other._filePath);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileShowCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _filePath.GetHashCode();
    }
}