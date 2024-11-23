using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers.GoToChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers;

public class TreeGoToHandler : BaseTreeChainElement
{
    private const string CommandName = "goto";
    private readonly ContextTools _contextTools;
    private readonly IGoToChainElement _next;

    public TreeGoToHandler(IGoToChainElement next, ContextTools contextTools)
    {
        _contextTools = contextTools;
        _next = next;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest)
    {
        if (!commandRequest.Current.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
        {
            return Next?.Handle(commandRequest);
        }

        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("Tree GoTo command must have one argument, zero provided");
            return null;
        }

        return _next.Handle(commandRequest, new TreeGotoCommand.TreeGotoCommandBuilder());
    }
}