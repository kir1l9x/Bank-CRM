using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers.CopyChain;

public class CopySourcePathHandler : BaseCopyChainElement
{
    private readonly ContextTools _contextTools;

    public CopySourcePathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileCopyCommand.FileCopyCommandBuilder builder)
    {
        builder.AddSourcePath(new SystemPath(_contextTools, commandRequest.Current, new SystemPathValidation()));
        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("File copy command must have two arguments, one provided");
            return null;
        }

        return Next?.Handle(commandRequest, builder);
    }
}