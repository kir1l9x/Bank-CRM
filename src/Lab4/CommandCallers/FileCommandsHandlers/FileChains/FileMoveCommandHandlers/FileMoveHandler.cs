using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers;

public class FileMoveHandler : BaseFileChainElement
{
    private const string CommandName = "move";
    private readonly ContextTools _contextTools;
    private readonly IMoveChainElement _next;

    public FileMoveHandler(IMoveChainElement next, ContextTools contextTools)
    {
        _next = next;
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest)
    {
        if (!commandRequest.Current.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
        {
            return Next?.Handle(commandRequest);
        }

        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("File move command must have two arguments, zero provided");
            return null;
        }

        return _next.Handle(commandRequest, FileMoveCommand.Builder());
    }
}