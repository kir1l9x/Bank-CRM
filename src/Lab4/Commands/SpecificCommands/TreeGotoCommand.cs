using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class TreeGotoCommand : ICommand
{
    private readonly ISystemPath _goToPath;

    private TreeGotoCommand(ISystemPath goToPath)
    {
        _goToPath = goToPath;
    }

    public static TreeGotoCommandBuilder Builder()
    {
        return new TreeGotoCommandBuilder();
    }

    public class TreeGotoCommandBuilder
    {
        private ISystemPath? _goToPath;

        public TreeGotoCommandBuilder AddPathToGo(ISystemPath goToPath)
        {
            _goToPath = goToPath;
            return this;
        }

        public ICommand Build()
        {
            return new TreeGotoCommand(_goToPath ?? throw new ArgumentNullException(nameof(_goToPath)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.ToGoTo(_goToPath);
    }

    public bool Equals(TreeGotoCommand? other)
    {
        if (other == null) return false;

        return _goToPath.Equals(other._goToPath);
    }

    public override bool Equals(object? obj)
    {
        return obj is TreeGotoCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _goToPath.GetHashCode();
    }
}