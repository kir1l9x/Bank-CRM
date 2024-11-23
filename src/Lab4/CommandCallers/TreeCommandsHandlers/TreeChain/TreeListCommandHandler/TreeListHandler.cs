using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler;

public class TreeListHandler : BaseTreeChainElement
{
    private const string CommandName = "list";
    private readonly ContextTools _contextTools;
    private readonly IListChainElement _next;

    public TreeListHandler(IListChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("Tree list command must have flag");
            return null;
        }

        return _next.Handle(commandRequest, new TreeListCommand.TreeListCommandBuilder());
    }
}