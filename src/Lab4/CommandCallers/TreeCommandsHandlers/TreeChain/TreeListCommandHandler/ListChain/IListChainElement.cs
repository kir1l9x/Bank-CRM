using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.TreeCommandsHandlers.TreeChain.TreeListCommandHandler.ListChain;

public interface IListChainElement
{
    IListChainElement AddNext(IListChainElement listChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, TreeListCommand.TreeListCommandBuilder builder);
}