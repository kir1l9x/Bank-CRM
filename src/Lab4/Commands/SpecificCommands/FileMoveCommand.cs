using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class FileMoveCommand : ICommand
{
    private readonly ISystemPath _sourcePath;
    private readonly ISystemPath _destinationPath;

    private FileMoveCommand(ISystemPath sourcePath, ISystemPath destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public static FileMoveCommandBuilder Builder()
    {
        return new FileMoveCommandBuilder();
    }

    public class FileMoveCommandBuilder
    {
        private ISystemPath? _sourcePath;
        private ISystemPath? _destinationPath;

        public FileMoveCommandBuilder AddSourcePath(ISystemPath sourcePath)
        {
            _sourcePath = sourcePath;
            return this;
        }

        public FileMoveCommandBuilder AddDestinationPath(ISystemPath destinationPath)
        {
            _destinationPath = destinationPath;
            return this;
        }

        public ICommand Build()
        {
            return new FileMoveCommand(
                _sourcePath ?? throw new ArgumentNullException(nameof(_sourcePath)),
                _destinationPath ?? throw new ArgumentNullException(nameof(_destinationPath)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.MoveFile(
            _sourcePath.Path ?? throw new Exception(),
            _destinationPath.Path ?? throw new Exception());
    }

    public bool Equals(FileMoveCommand? other)
    {
        if (other == null) return false;

        return _sourcePath.Equals(other._sourcePath) && _destinationPath.Equals(other._destinationPath);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileMoveCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_sourcePath, _destinationPath);
    }
}