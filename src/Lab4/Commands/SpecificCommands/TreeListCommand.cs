namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class TreeListCommand : ICommand
{
    private readonly int _depth;

    private TreeListCommand(int depth)
    {
        _depth = depth;
    }

    public static TreeListCommandBuilder Builder()
    {
        return new TreeListCommandBuilder();
    }

    public class TreeListCommandBuilder
    {
        private int? _depth;

        public TreeListCommandBuilder SetDepth(int depth)
        {
            _depth = depth;
            return this;
        }

        public ICommand Build()
        {
            return new TreeListCommand(_depth ?? throw new ArgumentNullException(nameof(_depth)));
        }
    }

    public void Execute(Context context)
    {
        context.FileSystem?.TreeList(_depth);
    }

    public bool Equals(TreeListCommand? other)
    {
        if (other == null) return false;

        return _depth == other._depth;
    }

    public override bool Equals(object? obj)
    {
        return obj is TreeListCommand other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _depth.GetHashCode();
    }
}