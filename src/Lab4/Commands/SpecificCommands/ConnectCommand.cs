using Itmo.ObjectOrientedProgramming.Lab4.FileSystemModes;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class ConnectCommand : ICommand
{
    private readonly ISystemPath _systemConnectPath;

    private ConnectCommand(ISystemPath systemPath, FileSystemMode fileSystemMode)
    {
        _systemConnectPath = systemPath;
    }

    public static ConnectCommandBuilder Builder()
    {
        return new ConnectCommandBuilder();
    }

    public class ConnectCommandBuilder
    {
        private ISystemPath? _systemConnectPath;
        private FileSystemMode? _connectionMode;

        public ConnectCommandBuilder AddConnectionPath(ISystemPath systemPath)
        {
            _systemConnectPath = systemPath;
            return this;
        }

        public ConnectCommandBuilder AddConnectionMode(FileSystemMode fileSystemMode)
        {
            _connectionMode = fileSystemMode;
            return this;
        }

        public ICommand Build()
        {
            return new ConnectCommand(
                _systemConnectPath ?? throw new ArgumentNullException(nameof(_systemConnectPath)),
                _connectionMode ?? new FileSystemMode.Local());
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.Connect(_systemConnectPath);
    }

    public bool Equals(ConnectCommand? other)
    {
        if (other == null) return false;

        return _systemConnectPath.Equals(other._systemConnectPath);
    }

    public override bool Equals(object? obj)
    {
        return obj is ConnectCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _systemConnectPath.GetHashCode();
    }
}