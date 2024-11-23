using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers;

public class FileShowHandler : BaseFileChainElement
{
    private const string CommandName = "show";
    private readonly ContextTools _contextTools;
    private readonly IShowChainElement _next;

    public FileShowHandler(IShowChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("File show command must have one argument, zero provided");
            return null;
        }

        return _next.Handle(commandRequest, FileShowCommand.Builder());
    }
}