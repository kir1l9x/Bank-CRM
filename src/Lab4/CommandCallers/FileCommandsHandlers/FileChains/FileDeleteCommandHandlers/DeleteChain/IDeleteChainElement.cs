using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileDeleteCommandHandlers.DeleteChain;

public interface IDeleteChainElement
{
    IDeleteChainElement AddNext(IDeleteChainElement deleteChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, FileDeleteCommand.FileDeleteCommandBuilder builder);
}