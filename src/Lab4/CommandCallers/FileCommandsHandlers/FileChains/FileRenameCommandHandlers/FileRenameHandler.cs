using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers;

public class FileRenameHandler : BaseFileChainElement
{
    private const string CommandName = "rename";
    private readonly ContextTools _contextTools;
    private readonly IRenameChainElement _next;

    public FileRenameHandler(IRenameChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("File rename command must have two arguments, zero provided");
            return null;
        }

        return _next.Handle(commandRequest, FileRenameCommand.Builder());
    }
}