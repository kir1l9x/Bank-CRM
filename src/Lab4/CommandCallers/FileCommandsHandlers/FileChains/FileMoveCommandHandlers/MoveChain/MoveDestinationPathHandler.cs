using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileMoveCommandHandlers.MoveChain;

public class MoveDestinationPathHandler : BaseMoveChainElement
{
    private readonly ContextTools _contextTools;

    public MoveDestinationPathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileMoveCommand.FileMoveCommandBuilder builder)
    {
        builder.AddDestinationPath(new SystemPath(_contextTools, commandRequest.Current, new DirectoryPathValidation()));

        return builder.Build();
    }
}