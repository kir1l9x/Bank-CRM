using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeGoToCommandHandlers.GoToChain;

public interface IGoToChainElement
{
    IGoToChainElement AddNext(IGoToChainElement goToChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, TreeGotoCommand.TreeGotoCommandBuilder builder);
}