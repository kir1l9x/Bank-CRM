using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileCopyCommandHandlers.CopyChain;

public class CopyDestinationPathHandler : BaseCopyChainElement
{
    private readonly ContextTools _contextTools;

    public CopyDestinationPathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileCopyCommand.FileCopyCommandBuilder builder)
    {
        builder.AddDestinationPath(new SystemPath(_contextTools, commandRequest.Current, new DirectoryPathValidation()));

        return builder.Build();
    }
}