using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileRenameCommandHandlers.RenameChain;

public class RenamePathHandler : BaseRenameChainElement
{
    private readonly ContextTools _contextTools;

    public RenamePathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileRenameCommand.FileRenameCommandBuilder builder)
    {
        builder.AddFilePath(new SystemPath(_contextTools, commandRequest.Current, new SystemPathValidation()));
        if (!commandRequest.MoveNext())
        {
            _contextTools.Writer.Write("File rename command must have two arguments, one provided");
            return null;
        }

        return Next?.Handle(commandRequest, builder);
    }
}