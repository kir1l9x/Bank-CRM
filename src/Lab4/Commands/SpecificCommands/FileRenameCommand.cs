using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class FileRenameCommand : ICommand
{
    private readonly ISystemPath _filePath;
    private readonly string _newFileName;

    private FileRenameCommand(ISystemPath filePath, string newFileName)
    {
        _filePath = filePath;
        _newFileName = newFileName;
    }

    public static FileRenameCommandBuilder Builder()
    {
        return new FileRenameCommandBuilder();
    }

    public class FileRenameCommandBuilder
    {
        private ISystemPath? _filePath;
        private string? _newFileName;

        public FileRenameCommandBuilder AddFilePath(ISystemPath filePath)
        {
            _filePath = filePath;
            return this;
        }

        public FileRenameCommandBuilder AddNewFileName(string newFileName)
        {
            _newFileName = newFileName;
            return this;
        }

        public ICommand Build()
        {
            return new FileRenameCommand(
                _filePath ?? throw new ArgumentNullException(nameof(_filePath)),
                _newFileName ?? throw new ArgumentNullException(nameof(_newFileName)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.RenameFile(
            _filePath.Path ?? throw new Exception(),
            _newFileName);
    }

    public bool Equals(FileRenameCommand? other)
    {
        if (other == null) return false;

        return _filePath.Equals(other._filePath) && _newFileName == other._newFileName;
    }

    public override bool Equals(object? obj)
    {
        return obj is FileRenameCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_filePath, _newFileName);
    }
}