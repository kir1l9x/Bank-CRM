using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers;

public class TreeCommandHandler : CommandHandlerBase
{
    private const string CommandName = "tree";
    private readonly ITreeChainElement _next;
    private readonly ContextTools _contextTools;

    public TreeCommandHandler(ITreeChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("Tree command must have a type: list/goto");
            return null;
        }

        return _next.Handle(commandRequest);
    }
}