using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;

public class MoveSourcePathHandler : BaseMoveChainElement
{
    private readonly ContextTools _contextTools;

    public MoveSourcePathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileMoveCommand.FileMoveCommandBuilder builder)
    {
        builder.AddSourcePath(new SystemPath(_contextTools, commandRequest.Current, new SystemPathValidation()));
        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("File move command must have two arguments, one provided");
            return null;
        }

        return Next?.Handle(commandRequest, builder);
    }
}