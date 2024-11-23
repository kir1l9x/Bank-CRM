using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;

public class ShowPathHandler : BaseShowChainElement
{
    private readonly ContextTools _contextTools;

    public ShowPathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileShowCommand.FileShowCommandBuilder builder)
    {
        builder.AddFilePath(new SystemPath(_contextTools, commandRequest.Current, new FilePathValidation()));
        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("File show command must have two arguments, one provided");
            return null;
        }

        return Next?.Handle(commandRequest, builder);
    }
}