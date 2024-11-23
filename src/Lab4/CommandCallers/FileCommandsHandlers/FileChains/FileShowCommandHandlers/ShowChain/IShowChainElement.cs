using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.SpecificCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandCallers.FileCommandsHandlers.FileChains.FileShowCommandHandlers.ShowChain;

public interface IShowChainElement
{
    IShowChainElement AddNext(IShowChainElement showChainElement);

    ICommand? Handle(IEnumerator<string> commandRequest, FileShowCommand.FileShowCommandBuilder builder);
}