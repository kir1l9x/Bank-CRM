using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers.DeleteChain;

public class DeletePathHandler : BaseDeleteChainElement
{
    private readonly ContextTools _contextTools;

    public DeletePathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, FileDeleteCommand.FileDeleteCommandBuilder builder)
    {
        builder.AddPathToDelete(new SystemPath(_contextTools, commandRequest.Current, new SystemPathValidation()));

        return builder.Build();
    }
}