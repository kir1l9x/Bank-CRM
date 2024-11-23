namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

public class DisconnectCommand : ICommand
{
    public void Execute(Context context)
    {
        context.FileSystem?.Disconnect();
    }

    public bool Equals(DisconnectCommand? other)
    {
        return other != null;
    }

    public override bool Equals(object? obj)
    {
        return obj is DisconnectCommand;
    }

    public override int GetHashCode()
    {
        return typeof(DisconnectCommand).GetHashCode();
    }
}