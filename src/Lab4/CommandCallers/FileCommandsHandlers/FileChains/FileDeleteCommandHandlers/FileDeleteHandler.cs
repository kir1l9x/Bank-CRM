using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers.DeleteChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers;

public class FileDeleteHandler : BaseFileChainElement
{
    private const string CommandName = "delete";
    private readonly ContextTools _contextTools;
    private readonly IDeleteChainElement _next;

    public FileDeleteHandler(IDeleteChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("File delete command must have one argument, zero provided");
            return null;
        }

        return _next.Handle(commandRequest, FileDeleteCommand.Builder());
    }
}