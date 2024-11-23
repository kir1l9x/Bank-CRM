using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class FileCopyCommand : ICommand
{
    private readonly ISystemPath _sourcePath;
    private readonly ISystemPath _destinationPath;

    private FileCopyCommand(ISystemPath sourcePath, ISystemPath destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public static FileCopyCommandBuilder Builder()
    {
        return new FileCopyCommandBuilder();
    }

    public class FileCopyCommandBuilder
    {
        private ISystemPath? _sourcePath;
        private ISystemPath? _destinationPath;

        public FileCopyCommandBuilder AddSourcePath(ISystemPath sourcePath)
        {
            _sourcePath = sourcePath;
            return this;
        }

        public FileCopyCommandBuilder AddDestinationPath(ISystemPath destinationPath)
        {
            _destinationPath = destinationPath;
            return this;
        }

        public ICommand Build()
        {
            return new FileCopyCommand(
                _sourcePath ?? throw new ArgumentNullException(nameof(_sourcePath)),
                _destinationPath ?? throw new ArgumentNullException(nameof(_destinationPath)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.CopyFile(
            _sourcePath.Path ?? throw new Exception(),
            _destinationPath.Path ?? throw new Exception());
    }

    public bool Equals(FileCopyCommand? other)
    {
        if (other == null) return false;

        return _sourcePath.Equals(other._sourcePath) && _destinationPath.Equals(other._destinationPath);
    }

    public override bool Equals(object? obj)
    {
        return obj is FileCopyCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_sourcePath, _destinationPath);
    }
}