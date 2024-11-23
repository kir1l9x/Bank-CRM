using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths;
using Itmo.ObjectOrientedProgramming.Lab4.SystemPaths.PathValidation;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers.GoToChain;

public class GoToPathHandler : BaseGoToChainElement
{
    private readonly ContextTools _contextTools;

    public GoToPathHandler(ContextTools contextTools)
    {
        _contextTools = contextTools;
    }

    public override ICommand? Handle(IEnumerator<string> commandRequest, TreeGotoCommand.TreeGotoCommandBuilder builder)
    {
        builder.AddPathToGo(new SystemPath(_contextTools, commandRequest.Current, new DirectoryPathValidation()));

        return builder.Build();
    }
}