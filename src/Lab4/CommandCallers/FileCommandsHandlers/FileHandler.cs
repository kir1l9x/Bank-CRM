using Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers;

public class FileHandler : CommandHandlerBase
{
    private const string CommandName = "file";
    private readonly IFileChainElement _next;
    private readonly ContextTools _contextTools;

    public FileHandler(IFileChainElement next, ContextTools contextTools)
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
            _contextTools.Writer.Write("File command must have a type: copy/delete/show/rename/move");
            return null;
        }

        return _next.Handle(commandRequest);
    }
}