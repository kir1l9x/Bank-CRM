using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class FileDeleteCommand : ICommand
{
    private readonly ISystemPath _path;

    private FileDeleteCommand(ISystemPath path)
    {
        _path = path;
    }

    public static FileDeleteCommandBuilder Builder()
    {
        return new FileDeleteCommandBuilder();
    }

    public class FileDeleteCommandBuilder
    {
        private ISystemPath? _path;

        public FileDeleteCommandBuilder AddPathToDelete(ISystemPath path)
        {
            _path = path;
            return this;
        }

        public ICommand Build()
        {
            return new FileDeleteCommand(_path ?? throw new ArgumentNullException(nameof(_path)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.DeleteFile(_path.Path ?? throw new Exception());
    }

    public bool Equals(FileDeleteCommand? other)
    {
        if (other == null) return false;

        return _path.Equals(other._path);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileDeleteCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _path.GetHashCode();
    }
}