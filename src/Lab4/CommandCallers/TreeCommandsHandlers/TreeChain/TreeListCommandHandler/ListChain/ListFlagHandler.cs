using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;

public class ListFlagHandler : BaseListChainElement
{
    private const string Flag = "-d";
    private readonly ContextTools _contextTools;

    public ListFlagHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, TreeListCommand.TreeListCommandBuilder builder)
    {
        if (commandRequest.Current.Equals(Flag, StringComparison.OrdinalIgnoreCase))
        {
            if (!commandRequest.MoveNext())
            {
                _contextTools.Writer.Write("default value = 1 set to list depth");
                return builder.Build();
            }
        }

        return Next?.Handle(commandRequest, builder);
    }
}