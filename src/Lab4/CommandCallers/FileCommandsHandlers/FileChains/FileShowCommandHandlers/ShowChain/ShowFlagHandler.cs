using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;

public class ShowFlagHandler : BaseShowChainElement
{
    private const string Flag = "-m";
    private readonly ContextTools _contextTools;

    public ShowFlagHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileShowCommand.FileShowCommandBuilder builder)
    {
        if (commandRequest.Current.Equals(Flag, StringComparison.OrdinalIgnoreCase))
        {
            if (!commandRequest.MoveNext())
            {
                _contextTools.Writer.Write("Show command must have a mode");
                return null;
            }
        }

        return Next?.Handle(commandRequest, builder);
    }
}